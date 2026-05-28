import { ObjectId } from "mongodb";
import DbConnection from "./mongoAccess.js";

const TaskAccess = 
{

    async userTasks() 
    {
        let db = await DbConnection.Get();
        return db.collection("userTasks");
    },

    async tasksResult() 
    {
        const db = await DbConnection.Get();
        return db.collection("tasksResult");
    },
    
    async removePipeline(mongoId)
    {
        const collection = await this.userTasks();
        const result = await collection.deleteOne({_id : new ObjectId(mongoId)});
    },

    async addUserTask(json)
    {
        const collection = await this.userTasks();
        return await collection.insertOne(json);
    },

    async getAllTasks() 
    {
        const collection = await this.userTasks();
        return await collection.find({}).toArray();
    },

    async addTaskToPipe(taskJson , pipeId) 
    {
        const collection = await this.userTasks();
        return await collection.updateOne(
            { _id: new ObjectId(pipeId) },
            { $push: { taskList: taskJson }}
        );
    },

    async getUserTasks(userId) 
    {
        const collection = await this.userTasks();
        return await collection.find({idOwner : userId}).toArray();
    },

    async addTaskResult(resultJson) 
    {
        const collection = await this.tasksResult();
        return await collection.insertOne(resultJson);
    },

    async getTaskResults(userId) 
    {
        const collection = await this.tasksResult();
        return await collection.find({idOwner : userId}).toArray();
    },

    async getPipelineById(pipeId)
    {
        const collection = await this.userTasks();
        return await collection.findOne({ _id: new ObjectId(pipeId) });
    },
};

export default TaskAccess;