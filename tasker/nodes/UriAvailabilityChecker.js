
const UriChecker = 
{
    

    // this function receive task.parameter and must ensure that the received data is enough to run the check by returning true or false in the final obj
    async assertData(rawData)
    {
        if(rawData.uri && typeof rawData.uri === "string" )
            // here i return raw as formatted because no work is needed
            return { canRun : true , formattedData : rawData}
        return { canRun : false }

    },

    async run(checkedData)
    {
        try
        {
            // if the data assertion worked the test is runned
            const fetchResult = await fetch(checkedData.uri);
            return fetchResult.status === 200;
        }
        catch
        {
            return false;
        }
    },
}

export default UriChecker;