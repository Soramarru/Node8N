import { ObjectId } from "mongodb";
import DbConnection from "./mongoAccess.js";

const TaskAccess = 
{

    async userTasks() 
    {
        const db = await DbConnection.Get();
        return db.collection("userTasks");
    },

    async tasksResult() 
    {
        const db = await DbConnection.Get();
        return db.collection("tasksResult");
    },

    async addTaskToPipe(taskJson , pipeId) 
    {
        const collection = await this.userTasks();
        /*

            {
                acknowledged: true,
                insertedId: new ObjectId('69fc59cc32adeb4d85e05b4c')
            }
        */
        return await collection.updateOne(
            { _id: new ObjectId(pipeId) },
            { $push: { taskList: taskJson }}
        );
    },

    async getUserTasks(userId) 
    {
        const collection = await this.userTasks();
        return await collection.find({userId : userId}).toArray();
    },

    async addTaskResult(resultJson) 
    {
        const collection = await this.tasksResult();
        return await collection.insertOne(resultJson);
    },

    async getTaskResults(userId) 
    {
        const collection = await this.tasksResult();
        return await collection.find({userId : userId}).toArray();
    }
};

export default TaskAccess;