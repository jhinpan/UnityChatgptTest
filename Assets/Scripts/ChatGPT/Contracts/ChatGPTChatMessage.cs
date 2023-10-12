using System;
using Newtonsoft.Json;

[Serializable]
public class ChatGPTChatMessage
{
    
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }
 
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; }
}