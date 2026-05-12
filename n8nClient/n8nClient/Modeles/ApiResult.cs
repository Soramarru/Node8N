using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Modeles
{
    public class ApiResult(string status, string message, dynamic data)
    {
        public string Status { get;  } = status;
        public string Message { get;  } = message;
        public dynamic Data { get;  } = data;

        public Color getColorFromStatus( )
        {
            return Status.ToLower() switch
            {
                "success" => Color.Green,
                "error" => Color.Red,
                "warning" => Color.Yellow,
                _ => Color.White
            };
        }
    }
}
