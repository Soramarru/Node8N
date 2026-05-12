import bcrypt from "bcrypt";
import crypto from "crypto";
import fs from 'fs/promises';
import userDBaccess from "../db/userDb.js";
import TaskAccess from "../mongoDb/taskAccess.js";
import { fileURLToPath } from "url";
import taskTypeEnum from "../models/taskTypeEnum.json" with {type: 'json'};
import path from "path";


const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

let saltRounds = 13;

let apiFunc =
{

    /**
     * receive name and password of a user if its not too big nor too small create it
     * @param {*} req 
     * @param {*} res 
     * @returns 
     */
    async createUser(req, res)
    {
        if (!req.body || !req.body.username || !req.body.password)
            return res.status(400).json({ status: "error", message: "Every input must be filled !", data: null });
        let { password, username } = req.body;

        if (!username || username.trim().length < 2)
            return res.status(400).json({ status: "error", message: "The username need to be at least 2 character!", data: null });
        if (username.trim().length > 50)
            return res.status(400).json({ status: "error", message: "The username must be less than 50 character", data: null });
        if (!password)
            return res.status(400).json({ status: "error", message: "Password must be filled", data: null });
        if (password.length < 5)
            return res.status(400).json({ status: "error", message: "The password need to be at least 5 character", data: null });
        if (await userDBaccess.usernameExist(username))
            return res.status(409).send({ status: "error", "message": "This user name already exist", data: null });

        let hashedPassword = await hashPassword(password);
        let newToken = await createUniqueToken();
        console.log(await userDBaccess.createUser(hashedPassword, username, newToken));
        res.status(200).send({ status: "success", "message": "Account created successfully", data: { username: username, token: newToken } });
    },

    /**
     * Receive the token and if it match one in db return the user info 
     * @param {*} req 
     * @param {*} res 
     * @returns 
     */
    async validateAuth(req,res)
    {
        if (!req.body || !req.body.token)
            return res.status(400).json({ status: "error", message: "You must send your token!", data: null });
        let { token } = req.body;
        let user = await userDBaccess.getUserTroughToken(token);
        if(user == false)
            return res.status(400).json({ status: "error", message: "Your token is not valid !", data: null });
        delete user.hashedPass;
        delete user.token;
        return res.status(200).json({ status: "success", message: "You are correctly authenticated", data: user  });

    },

    /**
     * receive the username and password of a user if it match with an existing one return token
     * @param {*} req 
     * @param {*} res 
     * @returns 
     */
    async loginUser(req, res)
    {
        if (!req.body || !req.body.username || !req.body.password)
            return res.status(400).json({ status: "error", message: "Every input must be filled !", data: null });
        let { password, username } = req.body;

        if (!username || username.trim().length < 2)
            return res.status(400).json({ status: "error", message: "The username need to be at least 2 character!", data: null });
        if (username.trim().length > 50)
            return res.status(400).json({ status: "error", message: "The username must be less than 50 character", data: null });
        if (!password)
            return res.status(400).json({ status: "error", message: "Password must be filled", data: null });
        if (password.length < 5)
            return res.status(400).json({ status: "error", message: "The password need to be at least 5 character", data: null });

        let user = await userDBaccess.getUserTroughName(username);
        if (user === false)
        {
            res.status(404).send({ status: "error", "message": "This user doesn't exist", data: {} });
            return;
        }
        if (await comparePasswords(password, user.hashedPass))
            res.status(200).send({ status: "success", "message": "Correctly authenticated", data: { username: user.username, token: user.token } });
        else
            res.status(403).send({ status: "error", "message": "The password doesn't match the username", data: {} });
    },

    async getReqList(req,res)
    {
        let folderReqPath = "../tasker/nodes/jsonRequirement";
        let dir = await fs.readdir(folderReqPath);
        res.status(200).send({ status: "success", "message": "fetch successfull", data: { } });
    },

    async getAllPipeline(req,res)
    {
        if (!req.body || !req.body.token)
            return res.status(400).json({ status: "error", message: "You must send your token!", data: null });
        let { token } = req.body;
        let user = await userDBaccess.getUserTroughToken(token);
        if(user == false)
            return res.status(400).json({ status: "error", message: "Your token is not valid !", data: null });
        // now that we have a valid user id query it in the mongodb
        let tasks = await TaskAccess.getUserTasks(user.idUser);
        res.status(200).send({ status: "success", "message": "Correctly authenticated", data: { tasks } });
        
    },

    async createPipeline(req,res)
    {
        
        if (!req.body || !req.body.token || !req.body.pipeName)
            return res.status(403).json({ status: "error", message: "You must send token and pipeName!", data: null });
        const {token , pipeName} = req.body;
        const user = await userDBaccess.getUserTroughToken(token);
        if(user === false)
            return res.status(403).json({ status: "error", message: "The provided token is not valid !", data: null });
        // everything needed now create the task and push it to mongo
        let pipeObject = { pipelineName : pipeName , idOwner : user.idUser , taskList : []}
        if(req.body.pipelineDesc)
            pipeObject.pipelineDesc = req.body.pipelineDesc;
        else
            pipeObject.pipelineDesc = "";
        let result =await TaskAccess.addUserTask(pipeObject);
        if(result.acknowledged)
            return res.status(200).send({ status: "success", "message": "Task successfully added", data: { id : result.insertedId} });
        else
            return res.status(505).send({ status: "error", "message": "A server side error occured while adding task please try again later", data: { } });
    },

    async addTaskToPipe(req,res)
    {
        if (!req.body || !req.body.token || !req.body.taskType|| !req.body.taskName|| !req.body.taskNodeName || !req.body.data || req.body.data.targetPipeId)
            return res.status(403).json({ status: "error", message: "You must send token taskType taskName , taskNodeName , data", data: null });
        const {token , taskType , taskName , taskNodeName , data , targetPipeId} = req.body;
        // token check
        const user = await userDBaccess.getUserTroughToken(token);
        if(user === false)
            return res.status(403).json({ status: "error", message: "The provided token is not valid !", data: null });
        // taskType check
        if(typeof taskTypeEnum[taskType] === "undefined") 
            return res.status(400).json({ status: "error", message: "The provided task type is not valid !" , data: taskTypeEnum });
        // this check tasknode name and do data check
        const taskCheck = await AssertJsonRequirement(taskType , taskNodeName , data);
        if(!taskCheck.valid)
            return res.status(400).json({ status: "error", message: taskCheck.message });
        // push it to mongoDb
        let response = await TaskAccess.addTaskToPipe( {taskType , taskName , taskNodeName , data} , targetPipeId);
        if(response.acknowledged)
            return res.status(200).send({ status: "success", "message": "In construction full check", data: { } });
        else
            return res.status(500).json({ status: "error", message: "An error occured on our side please try again later" , data: "" });
    }

}

async function AssertJsonRequirement(taskType, taskNodeName, data)
{
    const pathToRoot = taskTypeEnum[taskType];
    const reqFilePath = path.join(__dirname, "../../" + pathToRoot + "/jsonRequirement/" + taskNodeName + ".json");
    console.log(reqFilePath);
    
    let requirementJson;
    try
    {
        const fileContent = await fs.readFile(reqFilePath, "utf-8");
        requirementJson = JSON.parse(fileContent);
    }
    catch (e)
    {
        return { valid: false, message: "Could not load requirement file for node " + taskNodeName };
        console.error(e.message );
    }

    const fields = requirementJson.validationFields;

    for (const field of fields)
    {
        const { objId, required, wantedType, min, max } = field;
        const value = data[objId];

        if (required && (value === undefined || value === null || value === ""))
            return { valid: false, message: "Field " + objId + " is required" };

        if (value === undefined || value === null)
            continue;

        if (wantedType && typeof value !== wantedType)
            return { valid: false, message: "Field " + objId + " must be of type " + wantedType };

        if (wantedType === "string")
        {
            if (min !== undefined && value.length < min)
                return { valid: false, message: "Field " + objId + " must be at least " + min + " character(s) long" };
            if (max !== undefined && value.length > max)
                return { valid: false, message: "Field " + objId + " must be at most " + max + " character(s) long" };
        }
        else if (wantedType === "number")
        {
            if (min !== undefined && value < min)
                return { valid: false, message: "Field " + objId + " must be at least " + min };
            if (max !== undefined && value > max)
                return { valid: false, message: "Field " + objId + " must be at most " + max };
        }
    }
    return { valid: true, message: "Validation passed" };
}

async function hashPassword(password)
{
    const salt = await bcrypt.genSalt(saltRounds);
    return await bcrypt.hash(password, salt);
}

async function comparePasswords(userPassword, dbPassword)
{
    return await bcrypt.compare(userPassword, dbPassword);
}

async function createUniqueToken(length = 100 / 2)
{
    let newToken;
    do
        newToken = crypto.randomBytes(length).toString('hex');
    while (await userDBaccess.userTokenExist(newToken));
    return newToken;
}
export default apiFunc;