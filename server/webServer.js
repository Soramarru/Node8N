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
app.get("/nodes/requirement/list" , apiFunc.getReqList)
app.use("/nodes/requirement/" , express.static(path.join("../tasker/nodes/jsonRequirement")));

server.listen(port, () => 
{ 
  console.log("API server started on http://localhost:" + port);
});
