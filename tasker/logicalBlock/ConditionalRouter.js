const conditionalRouters =
{
    async assertData(data)
    {
        // case fail
        return { canRun: false }
        // case okay
        return { canRun: true, formattedData: data }
    },

    async run(checkedData, lastData)
    {
        const key = (typeof lastData === "boolean") ? String(lastData) : "default";

        if(checkedData[key] != undefined)
            return checkedData[key];

        return checkedData["default"];
    },
}
export default conditionalRouters;