using System;

[Serializable]
public class Event
{
    public string Icon;
    // public DateTime? EndDate;
    public string Description;

    public Event(string icon, string description) {
        Icon = icon;
        // EndDate = endDate;
        Description = description;
    }
}