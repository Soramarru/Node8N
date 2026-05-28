# Tasker

## how the tasker work

We will take a simple json for example

``` json
{
    "taskNodeName" : "UriAvailabilityChecker",
    "data" : {
        "uri" : "https://soramaru.fr"
    }
}
```
the "taskNodeName" must match exactly the name of the file in the nodes folder if its not found a error is thrown

in this example its only what is needed to run the test, in the server much more complex thing will occur ( eg a list of task with depending check for which one to do on which case )

the tasker will read the taskNodeName import it , send the data to check ,  throw a error if the data wasnt expected and if the data match will run the test and return what the test want to return (any)

## how to add your task

here is a js example of a task skeleton

```js
const UriChecker = 
{
    // this function receive task.parameter and must ensure that the received data is enough to run the check by returning true or false in the final obj
    async assertData(rawData)
    {
        // case fail 
        return { canRun : false }
        // case okay 
        return { canRun : true , formattedData : "data"}
    },

    // this will not be called at all if assertData does not return canRun : true
    async run(checkedData)
    {
        // if the data assertion worked the test is runned

        // if successfull
        return true;
        // if failed
        return false;

        // Note that if the "module" throw a crash the pipeline will completely stop
    },
}
export default UriChecker;
```

the assertData and the Run is two __**needed**__ function the task will throw and stop the pipeline if one of these two dont exist
( you can add as many other function in it and import what is needed )


## json requirement
the folder json requirement __**need**__ to contain a .json being the same name as the node otherwise it cant be communicated trough the api and your module can't be used , it must follow  ( as exemple here )

```

{
    "title" : "Temp checker",
    "description" : "Check the temperature trough a web api and return wether it is in a minimal threshold or not",
    "validationFields" : 
    [
        {
            "title" : "the title that will be displayed at the top",
            "labelText" : "the text the label will have in front of it",
            "placeholder" : "Can be empty",
            "required" : true,
            "wantedType" : "string/int/bool -> note that if left empty no check is done , and you will need to rely on the assert data",
            "min" : 1,
            "max" : 20
        },
        {
            "title" : "Minimal temperature",
            "labelText" : "min temp.",
            "placeholder" : "ex : 10",
            "required" : true,
            "wantedType" : "int",
            "min" : -20,
            "max" : 100
        }
    ]

}

```

**note** you can only put 1 type at a time <br>
**note** if its a string the min max is about the length <br>
**note** if its a int the min max is about the int itself <br>
**note** validation field required key is only title and labelText, but you will likely need heavy assert check afterward
