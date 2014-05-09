using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MoveShapeDemo
{
    public class Broadcaster
    {
        private readonly static Lazy<Broadcaster> _instance = new Lazy<Broadcaster>(() => new Broadcaster());
        private readonly TimeSpan _broadCastInterval = TimeSpan.FromMilliseconds(2000);
        private readonly IHubContext _hubContext;
        private Timer _broadcastLoop;
        private Shape _model;
        private bool _modelUpdated = false;

        private Broadcaster() {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<MoveShapeHub>();
            _model = new Shape();
            _broadcastLoop = new Timer(BroadcastShape,null,_broadCastInterval,_broadCastInterval );
        }

        public void BroadcastShape(object shape) {
            if (_modelUpdated)
            {
                _hubContext.Clients.AllExcept(_model.LastUpdatedBy).updateShape(_model);
                _modelUpdated = false;
            }
        }

        public void UpdateShape(Shape clientShape)
        {
            _model = clientShape;
            _modelUpdated = true;
        }

        public static Broadcaster Instance
        {
            get {
                return _instance.Value;
            }
        }
    }
}