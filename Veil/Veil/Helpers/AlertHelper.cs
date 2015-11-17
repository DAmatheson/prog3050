﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Veil.Helpers
{
    public enum AlertType
    {
        Error = 0,
        Warning = 1,
        Info = 2,
        Success = 3
    }

    public class AlertMessage
    {
        public AlertType Type { get; set; }
        public string Message { get; set; }

        /// <summary>
        ///     WARNING: This is rendered using Html.Raw. Do not include user input in this string
        /// </summary>
        public string Link { get; set; }

        public string AlertClass
        {
            get
            {
                switch (Type)
                {
                    case AlertType.Error:
                        return "alert";
                    case AlertType.Warning:
                        return "warning";
                    case AlertType.Info:
                        return "info";
                    case AlertType.Success:
                        return "success";
                    default:
                        return "";
                }
            }
        }
    }

    public static class AlertHelper
    {
        public static void AddAlert(this Controller controller, AlertType type, string message, string link = null)
        {
            List<AlertMessage> alerts = controller.TempData["AlertMessages"] as List<AlertMessage> ?? new List<AlertMessage>();
            alerts.Add(new AlertMessage() {Type = type, Message = message, Link = link});
            controller.TempData["AlertMessages"] = alerts;
        }

        public static void ClearAlerts(this Controller controller)
        {
            controller.TempData.Remove("AlertMessages");
        }
    }
}