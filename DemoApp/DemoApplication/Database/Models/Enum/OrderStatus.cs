﻿namespace DemoApplication.Database.Models.Enum
{
    public enum OrderStatus
    {
        None = 0,
        Created = 1,
        Approved = 2,
        Rejected = 4,
        Sent = 8,
        Completed = 16
    }
}
