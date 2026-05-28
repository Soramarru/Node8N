import startApi from "./server/webServer.js";
import tasker from "./tasker/tasker.js";
import TaskAccess from "./server/mongoDb/taskAccess.js";
async function init()
{
    await startApi();
    // Start cron loop
    let pipelines = await TaskAccess.getAllTasks();
    await tasker.initSchedules(pipelines);
}
init();