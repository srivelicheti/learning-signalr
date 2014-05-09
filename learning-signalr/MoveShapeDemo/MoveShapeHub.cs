using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveShapeDemo
{
    public class MoveShapeHub : Hub
    {
        private Broadcaster _broadcaster;
        public MoveShapeHub():this(Broadcaster.Instance)
        { }

        public MoveShapeHub(Broadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }
        public void UpdateShape(Shape clientShape)
        {
            clientShape.LastUpdatedBy = Context.ConnectionId;
            _broadcaster.UpdateShape(clientShape);
            //Clients.AllExcept(clientShape.LastUpdatedBy).updateShape(clientShape);
        }
    }

    public class Shape
    {
        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("top")]
        public double Top { get; set; }

        [JsonIgnore]
        public string LastUpdatedBy { get; set; }

    }
}