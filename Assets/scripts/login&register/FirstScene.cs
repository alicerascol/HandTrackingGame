﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour {

	public void Login() {
		SceneManager.LoadScene("Login");
	}

	public void Register() {
		SceneManager.LoadScene("Register");
	}

	public void PlayAsAGuest() {
		SceneManager.LoadScene("HitPlayButton");
	}
}
