using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenesManager))]
[RequireComponent(typeof(SaveManager))]
public class Managers : MonoBehaviour {

	public static ScenesManager Scene {get; private set;}
	public static SaveManager Save {get; private set;}

	private List<GameManager> _startSequence;

	void Awake() {
		Scene = GetComponent<ScenesManager>();
		Save = GetComponent<SaveManager>();

		_startSequence = new List<GameManager>();
		_startSequence.Add(Save);
		_startSequence.Add(Scene);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {

		foreach (GameManager manager in _startSequence) {
			manager.Startup();
		}
		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach (GameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			if (numReady > lastReady) {
				Debug.Log("Progress: " + numReady + "/" + numModules);
			}
			yield return null;
		}
	}
}