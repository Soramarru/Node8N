import example from "./example.json" with { type: "json" };

const tasker = 
{

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

    async executeTask(taskObj)
    {
        const { default: node } = await import("./nodes/" + taskObj.taskNodeName + ".js");
        let dataResult = await node.assertData(taskObj.data);
        if(dataResult.canRun == false)
            throw new Error("the task " + taskObj.taskName + " failed at check");
        let res = await node.run(dataResult.formattedData);
        return res;
    },

    async executeLogicalBlock(condObj ,savedResult)
    {
        const { default: logicalBlock }= await import("./logicalBlock/" + condObj.logicalBlockName + ".js");
        const nextTask = await logicalBlock.run(condObj.data, savedResult);
        return await this.getNextTask(nextTask, savedResult);
    },

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
tasker.executePipeline(example);
export default tasker;