import express from "express";
import path from "path";
import http from "http";
import cors from "cors";
import { fileURLToPath } from "url";

import apiFunc from "./api/apiFunc.js";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const port = 3000;
app.use(express.json());    
app.use(cors());

const server = http.createServer(app);

app.post("/auth/login" , apiFunc.loginUser)
app.post("/auth/register",  apiFunc.createUser )
app.post("/auth/validate",  apiFunc.validateAuth )


app.get("/nodes/requirement/list" , apiFunc.getNodesReqList)
app.use("/nodes/requirement/" , express.static(path.join("./tasker/nodes/jsonRequirement")));

app.get("/logicals/requirement/list" , apiFunc.getLogicalsReqList)
app.use("/logicals/requirement/" , express.static(path.join("./tasker/logicalBlock/jsonRequirement")));

app.post("/pipe/create" , apiFunc.createPipeline);
app.post("/pipe/get" , apiFunc.getAllPipeline);
app.post("/pipe/task/add" , apiFunc.addTaskToPipe);

async function startApi() 
{
  server.listen(port, () => 
  { 
    console.log("API server started on http://localhost:" + port);
  });
}
// C:\Users\soramaru\Desktop\Node\Node8N\tasker\logicalBlock\ConditionalRouter.js
export default startApi;