import bcrypt from "bcrypt";
import crypto from "crypto";
import fs from 'fs/promises';
import userDBaccess from "../db/userDb.js";
import TaskAccess from "../mongoDb/taskAccess.js";
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
        res.status(200).send({ status: "success", "message": "", data: { username: username, token: newToken } });
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
            res.status(200).send({ status: "success", "message": "", data: { username: user.username, token: user.token } });
        else
            res.status(403).send({ status: "error", "message": "The password doesn't match the username", data: {} });
    },

    async getReqList(req,res)
    {
        let folderReqPath = "../tasker/nodes/jsonRequirement";
        let dir = await fs.readdir(folderReqPath);
        res.status(200).send({ status: "success", "message": "fetch successfull", data: { } });
    },

    async createTask(req,res)
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
        let result =await TaskAccess.addUserTask(pipeObject);
        if(result.acknowledged)
        res.status(200).send({ status: "success", "message": "Task successfully added", data: { id : result.insertedId} });
        else
        res.status(505).send({ status: "error", "message": "A server side error occured while adding task please try again later", data: { } });


    }

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