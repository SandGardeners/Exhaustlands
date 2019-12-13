using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITagsParser 
{
    void ParseTag(string tagHeader, string content);
}
