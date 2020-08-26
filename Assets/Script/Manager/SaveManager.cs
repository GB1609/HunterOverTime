using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour, GameManager {
	public ManagerStatus status { get; private set; }

	public static SaveManager manager = null;

	public void Startup() {
		status = ManagerStatus.Started;
	}

	void Awake() {
		if (manager == null) {
			manager = this;
		} else if (manager != this) {
			Destroy(gameObject);
		}
	}

	public void Save() {
		string destination = Application.persistentDataPath + "Data.dat";
		FileStream file;

		if (File.Exists(destination))
			file = File.OpenWrite(destination);
		else
			file = File.Create(destination);

		// Vector3 pos = GameObject.Find("Player").transform.position;
		// Debug.Log("Saving..." + Managers.Player.health + " " + pos + " " + Managers.Scene.getCurrentScene() + " " + Managers.Player.bulletsLeft);
		// Data data = new Data(Managers.Player.health, pos, Managers.Scene.getCurrentScene(), Managers.Inventory._items, Managers.Player.currentBullets ,Managers.Player.bulletsLeft);
		// BinaryFormatter bf = new BinaryFormatter();
		// bf.Serialize(file, data);
		// file.Close();
	}

	public void Load() {
		Debug.Log("Loading");

		string destination = Application.persistentDataPath + "Data.dat";
		FileStream file;

		if (File.Exists(destination)) {
			file = File.OpenRead(destination);
		} else {
			Debug.LogError("File not found");
			return;
		}

		// BinaryFormatter bf = new BinaryFormatter();
		// Data data = (Data) bf.Deserialize(file);
		// file.Close();
		// Managers.Player.health = data.health;
		// Managers.Inventory._items = data.items;
		// Managers.Player.bulletsLeft = data.bulletsLeft;
		// Managers.Player.currentBullets = data.currentBullets;
		// Managers.Scene.ChangeSceneInstant(data.scene);
	}

}