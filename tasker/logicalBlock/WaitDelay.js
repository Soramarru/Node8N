const WaitDelay =
{
    async assertData(rawData)
    {
        let hours = Number(rawData.hours ?? 0);
        let minutes = Number(rawData.minutes ?? 0);
        if (isNaN(hours) || isNaN(minutes))
            return { canRun: false };
        if (hours < 0 || minutes < 0)
            return { canRun: false };
        if (hours === 0 && minutes === 0)
            return { canRun: false };
        return { canRun: true, formattedData: { ms: (hours * 60 + minutes) * 60 * 1000 } };
    },
    async run(checkedData, lastData)
    {
        // simple wait without blocking the js loop
        await new Promise(resolve => setTimeout(resolve, checkedData.ms));
        return lastData;
    },
}
export default WaitDelay;