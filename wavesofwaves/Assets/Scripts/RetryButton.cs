﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour {

    public void RetryGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
