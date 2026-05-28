
const webhookSender = 
{
    // this function receive task.parameter and must ensure that the received data is enough to run the check by returning true or false in the final obj
    async assertData(rawData)
    {
       if(rawData.webhook && rawData.message )
            return { canRun : true , formattedData : rawData}
        return { canRun : false }

    },

    async run(checkedData)
    {
        console.log(checkedData);
        const payload = this.createPayload(checkedData.message);
        const result = await fetch(checkedData.webhook, { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(payload) });
    },

    createPayload(message , name = "Node8N")
    {
         return {
            username: name,
            content: message,
         }
    }
}

export default webhookSender;