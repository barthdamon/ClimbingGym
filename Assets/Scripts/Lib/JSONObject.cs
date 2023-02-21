using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public abstract class JSONObject
{
	public abstract void ParseJSON(JToken json);
	public virtual void AppendJSON(ref JObject json) { }
	public virtual JObject ToJSON() { return new JObject(); }
}
