import bcrypt from "bcrypt";
import crypto from "crypto";
import fs from 'fs/promises';
import userDBaccess from "../db/userDb.js";
import TaskAccess from "../mongoDb/taskAccess.js";
import { fileURLToPath } from "url";
import taskTypeEnum from "../models/taskTypeEnum.json" with {type: 'json'};
import path from "path";
import tasker from "../../tasker/tasker.js";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

let saltRounds = 13;
let ApiWorker = 
{
    // how many time should it salt the password    
    saltRounds : 13,

    /**
     * Get the required json requirement and ask a function to run all checks , notify user of success or not
     * @param {*} taskType 
     * @param {*} taskNodeName 
     * @param {*} data 
     * @returns 
     */
    async AssertJsonRequirement(taskType, taskNodeName, data)
    {
        const pathToRoot = taskTypeEnum[taskType];
        const reqFilePath = path.join(__dirname, "../../" + pathToRoot + "/jsonRequirement/" + taskNodeName + ".json" );
        
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
            const value = data[field.objId];
            let checkValue = await this.fieldValidator(field , value);
            if(!checkValue.valid)
                return checkValue;
        }
        return { valid: true, message: "Validation passed" };
    },

    /**
     * Check wether the received data match the task requirement written on its json file
     * @param {*} field 
     * @param {*} value 
     * @returns 
     */
    async fieldValidator(field, value)
    {
        console.log(field);
        
        let { objId, required, wantedType, min, max } = field;
        if (required && (value === undefined || value === null || value === ""))
            return { valid: false, message: "Field " + objId + " is required" };

        if (value === undefined || value === null)
            return { valid: false, message: "The value cant be null." };
        // need this traduction
        if(wantedType == "int")
            wantedType = "number";
        
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
        return { valid: true };
    },

    /**
     * Simple bcrypt hasher
     * @param {*} password 
     * @returns 
     */
    async hashPassword(password)
    {
        const salt = await bcrypt.genSalt(saltRounds);
        return await bcrypt.hash(password, salt);
    },

    /**
     * Simple bcrypt password/hash checker
     * @param {*} userPassword 
     * @param {*} dbPassword 
     * @returns 
     */
    async  comparePasswords(userPassword, dbPassword)
    {
        return await bcrypt.compare(userPassword, dbPassword);
    },

    /**
     * Create a unique token using random data , shouldDbCheck is used only on test file so it doesnt need to run db request
     * @param {*} length 
     * @param {*} shouldDbCheck 
     * @returns 
     */
    async  createUniqueToken(length = 100 / 2 , shouldDbCheck = true)
    {
        let newToken;
        do
            newToken = crypto.randomBytes(length).toString('hex');
        while (shouldDbCheck && await userDBaccess.userTokenExist(newToken));
        return newToken;
    },
}
export default ApiWorker;