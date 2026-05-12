import { MongoClient } from 'mongodb';
import config from "../../data.json" with {type: 'json'};

let DbConnection = 
{

    db : null,
    async DbConnect() 
    {
        try 
        {
            const client = await MongoClient.connect(config.mongodbUri);
            this.db = client.db("node8n");
            return this.db;
        } catch (e) 
        {
            return e;
        }
    },

   async Get() 
   {
        if (this.db != null) 
            return this.db;
        else 
        {
            this.db = await this.DbConnect();
            return this.db; 
        }
    }

}


export default DbConnection;
