import cron from "node-cron";
import example from "./example.json" with { type: "json" };

const tasker = 
{
    _jobs: new Map(),

    /**
     * Receive all known pipelines and start them, if a pipeline alread exist restart it
     * @param {*} pipelines 
     */
    async initSchedules(pipelines)
    {
        for (const pipeline of pipelines)
        {
            if(!pipeline.schedule)
                continue;
            if(!cron.validate(pipeline.schedule))
            {
                console.warn("invalid cron expression for pipeline : " + pipeline.pipelineName + " -> " + pipeline.schedule);
                continue;
            }
            const pipeId = pipeline._id?.toString();
            if(pipeId && this._jobs.has(pipeId))
            {
                this._jobs.get(pipeId).stop();
                this._jobs.delete(pipeId);
            }
            console.log("scheduling : " + pipeline.pipelineName + " -> " + pipeline.schedule);
            const job = cron.schedule(pipeline.schedule, async () =>
            {
                await tasker.executePipeline(pipeline);
            });
            if(pipeId)
                this._jobs.set(pipeId, job);
        }
    },

    /**
     * Execute the pipe task list by "resolving" what is the next task then run it
     * @param {*} json 
     */
    async executePipeline(json)
    {
        // get the task and execute it until it end
        console.log("executing : " , json.pipelineName);
        let savedResult = null;
        for (const element of json.taskList) 
        {  
            let task = await this.getNextTask(element, savedResult);
            savedResult = await this.executeTask(task);
        }
         
    },
    /**
     * Run the "assertData" of the task block and if it do not fail run it 
     * @param {*} taskObj 
     * @returns 
     */
    async executeTask(taskObj)
    {
        const { default: node } = await import("./nodes/" + taskObj.taskNodeName + ".js");
        let dataResult = await node.assertData(taskObj.data);
        if(dataResult.canRun == false)
            throw new Error("the task " + taskObj.taskName + " failed at check");
        let res = await node.run(dataResult.formattedData);
        return res;
    },

    /**
     * Run the block by fetching and importing the right file then start the "run" function that must be in there
     * @param {*} condObj 
     * @param {*} savedResult 
     * @returns 
     */
    async executeLogicalBlock(condObj ,savedResult)
    {
        const { default: logicalBlock }= await import("./logicalBlock/" + condObj.logicalBlockName + ".js");
        const nextTask = await logicalBlock.run(condObj.data, savedResult);
        return await this.getNextTask(nextTask, savedResult);
    },
    /**
     * Resolve what is the next task by checking the task type and starting the right resolver
     * @param {*} taskObj 
     * @param {*} savedResult 
     * @returns 
     */
    async getNextTask(taskObj , savedResult)
    {
        // check wether its a logical or a task
        if(taskObj.taskType == "task")
            return taskObj;
        // execute the logical until a type task is returned
        if(taskObj.taskType == "logicalBlock")
            return await this.executeLogicalBlock(taskObj , savedResult);
        else
            throw new Error("The task type was unexpected " , taskObj.taskType);
        // return task
    }
}
export default tasker;