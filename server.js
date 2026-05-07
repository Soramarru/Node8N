import startApi from "./server/webServer.js";
import tasker from "./tasker/tasker.js";

async function init()
{
    await startApi();
    // Start cron loop
}
init();