using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScoreObj{
    public string PersonAlias;
    public int Score;

    public SendScoreObj(string alias, int score)
    {
        this.PersonAlias = alias;
        this.Score = score;
    }


}
