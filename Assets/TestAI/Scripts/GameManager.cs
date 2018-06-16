﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    public Player[] players;

    private void Start() {
        instance = this;
    }
}
