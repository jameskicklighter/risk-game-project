using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public bool isGameActive = false;
	private TerritoryObject[] territoryArrayAll;
	public GameObject[] playerIDArray;
	public Color[] playerColors;
	private int turnPlayerID = 0;
	private int turnMode = 0; // 0 = Reinforce, 1 = Attack, 2 = Maneuver. 
	public TextMeshProUGUI[] info;

	// Territories.
	public TerritoryObject TheGift;
	public TerritoryObject Skagos;
	public TerritoryObject Karhold;
	public TerritoryObject TheDreadfort;
	public TerritoryObject Winterfell;
	public TerritoryObject WidowsWatch;
	public TerritoryObject WhiteHarbor;
	public TerritoryObject BearIsland;
	public TerritoryObject Wolfswood;
	public TerritoryObject Barrowlands;
	public TerritoryObject StoneyShore;
	public TerritoryObject TheNeck;
	public TerritoryObject CapeKraken;
	public TerritoryObject TheFingers;
	public TerritoryObject TheEyrie;
	public TerritoryObject MountainsOfTheMoon;
	public TerritoryObject Gulltown;
	public TerritoryObject TheTwins;
	public TerritoryObject TheTrident;
	public TerritoryObject Harrenhal;
	public TerritoryObject Riverrun;
	public TerritoryObject StoneySept;
	public TerritoryObject Harlaw;
	public TerritoryObject Pyke;
	public TerritoryObject TheCrag;
	public TerritoryObject GoldenTooth;
	public TerritoryObject CasterlyRock;
	public TerritoryObject Silverhill;
	public TerritoryObject Crakehall;
	public TerritoryObject CrackclawPoint;
	public TerritoryObject Dragonstone;
	public TerritoryObject KingsLanding;
	public TerritoryObject Kingswood;
	public TerritoryObject StormsEnd;
	public TerritoryObject Tarth;
	public TerritoryObject Rainwood;
	public TerritoryObject DornishMarshes;
	public TerritoryObject BlackwaterRush;
	public TerritoryObject TheMander;
	public TerritoryObject SearoadMarshes;
	public TerritoryObject Highgarden;
	public TerritoryObject Oldtown;
	public TerritoryObject ThreeTowers;
	public TerritoryObject TheArbor;
	public TerritoryObject RedMountains;
	public TerritoryObject Sandstone;
	public TerritoryObject Greenblood;
	public TerritoryObject Sunspear;

	// Start is called before the first frame update
	private void Start() {
		isGameActive = false;
		StartCoroutine(PauseStart());
	}

	IEnumerator PauseStart() {
		while (isGameActive == false) {
			yield return isGameActive;
		}
		if (isGameActive) {
			InitializeMapData();
			territoryArrayAll = TerritoryObject.territoryList.ToArray();
			ShuffleTerritoryArray(territoryArrayAll);
			InitializePlayerData();
			ScoreInfo();
		}
	}

	public void StartGame() {
		Debug.Log("Start button clicked.");
		isGameActive = true;
	}

	public void QuitGame() {
		isGameActive = false;
	}

	// Update is called once per frame
	private void Update() {
		if (isGameActive) {

		}
	}

	// First assign the territory game objects to an instance of TerritoryObject.
	// Then add adjacent territories to the instances via the function AddAdjTerritories.
	// Adjacencies include ports as this is the simplest way to account for them. 
	// (May be subject to change if other rules surrounding ports are added.)
	private void InitializeMapData() {
		TheGift = new TerritoryObject(GameObject.Find("Territory_TheGift"), false);
		Skagos = new TerritoryObject(GameObject.Find("Territory_Skagos"), false);
		Karhold = new TerritoryObject(GameObject.Find("Territory_Karhold"), false);
		TheDreadfort = new TerritoryObject(GameObject.Find("Territory_TheDreadfort"), true);
		Winterfell = new TerritoryObject(GameObject.Find("Territory_Winterfell"), true);
		WidowsWatch = new TerritoryObject(GameObject.Find("Territory_WidowsWatch"), false);
		WhiteHarbor = new TerritoryObject(GameObject.Find("Territory_WhiteHarbor"), false);
		BearIsland = new TerritoryObject(GameObject.Find("Territory_BearIsland"), false);
		Wolfswood = new TerritoryObject(GameObject.Find("Territory_Wolfswood"), false);
		Barrowlands = new TerritoryObject(GameObject.Find("Territory_Barrowlands"), true);
		StoneyShore = new TerritoryObject(GameObject.Find("Territory_StoneyShore"), false);
		TheNeck = new TerritoryObject(GameObject.Find("Territory_TheNeck"), false);
		CapeKraken = new TerritoryObject(GameObject.Find("Territory_CapeKraken"), false);
		TheFingers = new TerritoryObject(GameObject.Find("Territory_TheFingers"), false);
		TheEyrie = new TerritoryObject(GameObject.Find("Territory_TheEyrie"), true);
		MountainsOfTheMoon = new TerritoryObject(GameObject.Find("Territory_MountainsOfTheMoon"), false);
		Gulltown = new TerritoryObject(GameObject.Find("Territory_Gulltown"), false);
		TheTwins = new TerritoryObject(GameObject.Find("Territory_TheTwins"), true);
		TheTrident = new TerritoryObject(GameObject.Find("Territory_TheTrident"), false);
		Harrenhal = new TerritoryObject(GameObject.Find("Territory_Harrenhal"), true);
		Riverrun = new TerritoryObject(GameObject.Find("Territory_Riverrun"), true);
		StoneySept = new TerritoryObject(GameObject.Find("Territory_StoneySept"), false);
		Harlaw = new TerritoryObject(GameObject.Find("Territory_Harlaw"), false);
		Pyke = new TerritoryObject(GameObject.Find("Territory_Pyke"), true);
		TheCrag = new TerritoryObject(GameObject.Find("Territory_TheCrag"), false);
		GoldenTooth = new TerritoryObject(GameObject.Find("Territory_GoldenTooth"), false);
		CasterlyRock = new TerritoryObject(GameObject.Find("Territory_CasterlyRock"), true);
		Silverhill = new TerritoryObject(GameObject.Find("Territory_Silverhill"), false);
		Crakehall = new TerritoryObject(GameObject.Find("Territory_Crakehall"), true);
		CrackclawPoint = new TerritoryObject(GameObject.Find("Territory_CrackclawPoint"), false);
		Dragonstone = new TerritoryObject(GameObject.Find("Territory_Dragonstone"), true);
		KingsLanding = new TerritoryObject(GameObject.Find("Territory_KingsLanding"), true);
		Kingswood = new TerritoryObject(GameObject.Find("Territory_Kingswood"), false);
		StormsEnd = new TerritoryObject(GameObject.Find("Territory_StormsEnd"), true);
		Tarth = new TerritoryObject(GameObject.Find("Territory_Tarth"), false);
		Rainwood = new TerritoryObject(GameObject.Find("Territory_Rainwood"), false);
		DornishMarshes = new TerritoryObject(GameObject.Find("Territory_DornishMarshes"), true);
		BlackwaterRush = new TerritoryObject(GameObject.Find("Territory_BlackwaterRush"), false);
		TheMander = new TerritoryObject(GameObject.Find("Territory_TheMander"), true);
		SearoadMarshes = new TerritoryObject(GameObject.Find("Territory_SearoadMarshes"), false);
		Highgarden = new TerritoryObject(GameObject.Find("Territory_Highgarden"), true);
		Oldtown = new TerritoryObject(GameObject.Find("Territory_Oldtown"), true);
		ThreeTowers = new TerritoryObject(GameObject.Find("Territory_ThreeTowers"), false);
		TheArbor = new TerritoryObject(GameObject.Find("Territory_TheArbor"), false);
		RedMountains = new TerritoryObject(GameObject.Find("Territory_RedMountains"), false);
		Sandstone = new TerritoryObject(GameObject.Find("Territory_Sandstone"), true);
		Greenblood = new TerritoryObject(GameObject.Find("Territory_Greenblood"), false);
		Sunspear = new TerritoryObject(GameObject.Find("Territory_Sunspear"), true);
		// The Gift
		TheGift.AddAdjTerritory(Skagos);
		TheGift.AddAdjTerritory(Karhold);
		TheGift.AddAdjTerritory(TheDreadfort);
		TheGift.AddAdjTerritory(Winterfell);
		// Skagos
		Skagos.AddAdjTerritory(TheGift);
		Skagos.AddAdjTerritory(Karhold);
		// Karhold
		Karhold.AddAdjTerritory(TheGift);
		Karhold.AddAdjTerritory(Skagos);
		Karhold.AddAdjTerritory(TheDreadfort);
		// The Dreadfort
		TheDreadfort.AddAdjTerritory(TheGift);
		TheDreadfort.AddAdjTerritory(Karhold);
		TheDreadfort.AddAdjTerritory(Winterfell);
		TheDreadfort.AddAdjTerritory(WidowsWatch);
		// Winterfell
		Winterfell.AddAdjTerritory(TheGift);
		Winterfell.AddAdjTerritory(TheDreadfort);
		Winterfell.AddAdjTerritory(WidowsWatch);
		Winterfell.AddAdjTerritory(WhiteHarbor);
		Winterfell.AddAdjTerritory(BearIsland);
		Winterfell.AddAdjTerritory(Wolfswood);
		Winterfell.AddAdjTerritory(Barrowlands);
		// Widow's Watch
		WidowsWatch.AddAdjTerritory(TheDreadfort);
		WidowsWatch.AddAdjTerritory(Winterfell);
		WidowsWatch.AddAdjTerritory(WhiteHarbor);
		WidowsWatch.AddAdjTerritory(Gulltown);
		WidowsWatch.AddAdjTerritory(Dragonstone);
		WidowsWatch.AddAdjTerritory(KingsLanding);
		WidowsWatch.AddAdjTerritory(StormsEnd);
		WidowsWatch.AddAdjTerritory(Sunspear);
		// White Harbor
		WhiteHarbor.AddAdjTerritory(Winterfell);
		WhiteHarbor.AddAdjTerritory(WidowsWatch);
		WhiteHarbor.AddAdjTerritory(Barrowlands);
		WhiteHarbor.AddAdjTerritory(TheNeck);
		WhiteHarbor.AddAdjTerritory(Gulltown);
		WhiteHarbor.AddAdjTerritory(Dragonstone);
		WhiteHarbor.AddAdjTerritory(KingsLanding);
		WhiteHarbor.AddAdjTerritory(StormsEnd);
		WhiteHarbor.AddAdjTerritory(Sunspear);
		// Bear Island
		BearIsland.AddAdjTerritory(Winterfell);
		BearIsland.AddAdjTerritory(Wolfswood);
		BearIsland.AddAdjTerritory(StoneyShore);
		// Wolfswood
		Wolfswood.AddAdjTerritory(Winterfell);
		Wolfswood.AddAdjTerritory(BearIsland);
		Wolfswood.AddAdjTerritory(Barrowlands);
		Wolfswood.AddAdjTerritory(StoneyShore);
		Wolfswood.AddAdjTerritory(CapeKraken);
		Wolfswood.AddAdjTerritory(TheTrident);
		Wolfswood.AddAdjTerritory(Harlaw);
		Wolfswood.AddAdjTerritory(Pyke);
		Wolfswood.AddAdjTerritory(CasterlyRock);
		Wolfswood.AddAdjTerritory(Oldtown);
		// Barrowlands
		Barrowlands.AddAdjTerritory(Winterfell);
		Barrowlands.AddAdjTerritory(WhiteHarbor);
		Barrowlands.AddAdjTerritory(Wolfswood);
		Barrowlands.AddAdjTerritory(StoneyShore);
		Barrowlands.AddAdjTerritory(TheNeck);
		// Stoney Shore
		StoneyShore.AddAdjTerritory(BearIsland);
		StoneyShore.AddAdjTerritory(Wolfswood);
		StoneyShore.AddAdjTerritory(Barrowlands);
		// The Neck
		TheNeck.AddAdjTerritory(WhiteHarbor);
		TheNeck.AddAdjTerritory(Barrowlands);
		TheNeck.AddAdjTerritory(CapeKraken);
		TheNeck.AddAdjTerritory(TheTwins);
		TheNeck.AddAdjTerritory(Harlaw);
		// Cape Kraken
		CapeKraken.AddAdjTerritory(TheNeck);
		CapeKraken.AddAdjTerritory(Wolfswood);
		CapeKraken.AddAdjTerritory(TheTrident);
		CapeKraken.AddAdjTerritory(Harlaw);
		CapeKraken.AddAdjTerritory(Pyke);
		CapeKraken.AddAdjTerritory(CasterlyRock);
		CapeKraken.AddAdjTerritory(Oldtown);
		// The Fingers
		TheFingers.AddAdjTerritory(MountainsOfTheMoon);
		// The Eyrie
		TheEyrie.AddAdjTerritory(MountainsOfTheMoon);
		TheEyrie.AddAdjTerritory(Gulltown);
		// Mountains of the Moon
		MountainsOfTheMoon.AddAdjTerritory(TheFingers);
		MountainsOfTheMoon.AddAdjTerritory(TheEyrie);
		MountainsOfTheMoon.AddAdjTerritory(TheTwins);
		MountainsOfTheMoon.AddAdjTerritory(TheTrident);
		MountainsOfTheMoon.AddAdjTerritory(Harrenhal);
		// Gulltown
		Gulltown.AddAdjTerritory(WidowsWatch);
		Gulltown.AddAdjTerritory(WhiteHarbor);
		Gulltown.AddAdjTerritory(TheEyrie);
		Gulltown.AddAdjTerritory(Dragonstone);
		Gulltown.AddAdjTerritory(KingsLanding);
		Gulltown.AddAdjTerritory(StormsEnd);
		Gulltown.AddAdjTerritory(Sunspear);
		// The Twins
		TheTwins.AddAdjTerritory(TheNeck);
		TheTwins.AddAdjTerritory(MountainsOfTheMoon);
		TheTwins.AddAdjTerritory(TheTrident);
		TheTwins.AddAdjTerritory(Harlaw);
		// The Trident
		TheTrident.AddAdjTerritory(Wolfswood);
		TheTrident.AddAdjTerritory(CapeKraken);
		TheTrident.AddAdjTerritory(MountainsOfTheMoon);
		TheTrident.AddAdjTerritory(TheTwins);
		TheTrident.AddAdjTerritory(Harrenhal);
		TheTrident.AddAdjTerritory(Riverrun);
		TheTrident.AddAdjTerritory(Harlaw);
		TheTrident.AddAdjTerritory(Pyke);
		TheTrident.AddAdjTerritory(CasterlyRock);
		TheTrident.AddAdjTerritory(Oldtown);
		// Harrenhal
		Harrenhal.AddAdjTerritory(MountainsOfTheMoon);
		Harrenhal.AddAdjTerritory(TheTrident);
		Harrenhal.AddAdjTerritory(Riverrun);
		Harrenhal.AddAdjTerritory(StoneySept);
		Harrenhal.AddAdjTerritory(KingsLanding);
		Harrenhal.AddAdjTerritory(BlackwaterRush);
		// Riverrun
		Riverrun.AddAdjTerritory(TheTrident);
		Riverrun.AddAdjTerritory(Harrenhal);
		Riverrun.AddAdjTerritory(StoneySept);
		Riverrun.AddAdjTerritory(Pyke);
		Riverrun.AddAdjTerritory(TheCrag);
		Riverrun.AddAdjTerritory(GoldenTooth);
		// Stoney Sept
		StoneySept.AddAdjTerritory(Harrenhal);
		StoneySept.AddAdjTerritory(Riverrun);
		StoneySept.AddAdjTerritory(GoldenTooth);
		StoneySept.AddAdjTerritory(CasterlyRock);
		StoneySept.AddAdjTerritory(Silverhill);
		StoneySept.AddAdjTerritory(BlackwaterRush);
		// Harlaw
		Harlaw.AddAdjTerritory(Wolfswood);
		Harlaw.AddAdjTerritory(CapeKraken);
		Harlaw.AddAdjTerritory(TheNeck);
		Harlaw.AddAdjTerritory(TheTwins);
		Harlaw.AddAdjTerritory(TheTrident);
		Harlaw.AddAdjTerritory(Pyke);
		Harlaw.AddAdjTerritory(CasterlyRock);
		Harlaw.AddAdjTerritory(Oldtown);
		// Pyke
		Pyke.AddAdjTerritory(Wolfswood);
		Pyke.AddAdjTerritory(CapeKraken);
		Pyke.AddAdjTerritory(TheTrident);
		Pyke.AddAdjTerritory(Riverrun);
		Pyke.AddAdjTerritory(Harlaw);
		Pyke.AddAdjTerritory(TheCrag);
		Pyke.AddAdjTerritory(CasterlyRock);
		Pyke.AddAdjTerritory(Oldtown);
		// The Crag
		TheCrag.AddAdjTerritory(Riverrun);
		TheCrag.AddAdjTerritory(Pyke);
		TheCrag.AddAdjTerritory(GoldenTooth);
		TheCrag.AddAdjTerritory(CasterlyRock);
		// Golden Tooth
		GoldenTooth.AddAdjTerritory(Riverrun);
		GoldenTooth.AddAdjTerritory(StoneySept);
		GoldenTooth.AddAdjTerritory(TheCrag);
		GoldenTooth.AddAdjTerritory(CasterlyRock);
		// Casterly Rock
		CasterlyRock.AddAdjTerritory(Wolfswood);
		CasterlyRock.AddAdjTerritory(CapeKraken);
		CasterlyRock.AddAdjTerritory(TheTrident);
		CasterlyRock.AddAdjTerritory(StoneySept);
		CasterlyRock.AddAdjTerritory(Harlaw);
		CasterlyRock.AddAdjTerritory(Pyke);
		CasterlyRock.AddAdjTerritory(TheCrag);
		CasterlyRock.AddAdjTerritory(GoldenTooth);
		CasterlyRock.AddAdjTerritory(Silverhill);
		CasterlyRock.AddAdjTerritory(Crakehall);
		CasterlyRock.AddAdjTerritory(Oldtown);
		// Silverhill
		Silverhill.AddAdjTerritory(StoneySept);
		Silverhill.AddAdjTerritory(CasterlyRock);
		Silverhill.AddAdjTerritory(Crakehall);
		Silverhill.AddAdjTerritory(BlackwaterRush);
		Silverhill.AddAdjTerritory(SearoadMarshes);
		// Crakehall
		Crakehall.AddAdjTerritory(CasterlyRock);
		Crakehall.AddAdjTerritory(Silverhill);
		Crakehall.AddAdjTerritory(SearoadMarshes);
		// Crackclaw Point
		CrackclawPoint.AddAdjTerritory(Dragonstone);
		CrackclawPoint.AddAdjTerritory(KingsLanding);
		// Dragonstone
		Dragonstone.AddAdjTerritory(WidowsWatch);
		Dragonstone.AddAdjTerritory(WhiteHarbor);
		Dragonstone.AddAdjTerritory(Gulltown);
		Dragonstone.AddAdjTerritory(CrackclawPoint);
		Dragonstone.AddAdjTerritory(KingsLanding);
		Dragonstone.AddAdjTerritory(Kingswood);
		Dragonstone.AddAdjTerritory(StormsEnd);
		Dragonstone.AddAdjTerritory(Sunspear);
		// King's Landing
		KingsLanding.AddAdjTerritory(WidowsWatch);
		KingsLanding.AddAdjTerritory(WhiteHarbor);
		KingsLanding.AddAdjTerritory(Gulltown);
		KingsLanding.AddAdjTerritory(Harrenhal);
		KingsLanding.AddAdjTerritory(CrackclawPoint);
		KingsLanding.AddAdjTerritory(Dragonstone);
		KingsLanding.AddAdjTerritory(Kingswood);
		KingsLanding.AddAdjTerritory(StormsEnd);
		KingsLanding.AddAdjTerritory(BlackwaterRush);
		KingsLanding.AddAdjTerritory(Sunspear);
		// Kingswood
		Kingswood.AddAdjTerritory(Dragonstone);
		Kingswood.AddAdjTerritory(KingsLanding);
		Kingswood.AddAdjTerritory(StormsEnd);
		Kingswood.AddAdjTerritory(BlackwaterRush);
		Kingswood.AddAdjTerritory(TheMander);
		// Storm's End
		StormsEnd.AddAdjTerritory(WidowsWatch);
		StormsEnd.AddAdjTerritory(WhiteHarbor);
		StormsEnd.AddAdjTerritory(Gulltown);
		StormsEnd.AddAdjTerritory(Dragonstone);
		StormsEnd.AddAdjTerritory(KingsLanding);
		StormsEnd.AddAdjTerritory(Kingswood);
		StormsEnd.AddAdjTerritory(Tarth);
		StormsEnd.AddAdjTerritory(Rainwood);
		StormsEnd.AddAdjTerritory(DornishMarshes);
		StormsEnd.AddAdjTerritory(TheMander);
		StormsEnd.AddAdjTerritory(Sunspear);
		// Tarth
		Tarth.AddAdjTerritory(StormsEnd);
		Tarth.AddAdjTerritory(Rainwood);
		// Rainwood
		Rainwood.AddAdjTerritory(StormsEnd);
		Rainwood.AddAdjTerritory(Tarth);
		Rainwood.AddAdjTerritory(DornishMarshes);
		// Dornish Marshes
		DornishMarshes.AddAdjTerritory(StormsEnd);
		DornishMarshes.AddAdjTerritory(Rainwood);
		DornishMarshes.AddAdjTerritory(TheMander);
		DornishMarshes.AddAdjTerritory(Highgarden);
		DornishMarshes.AddAdjTerritory(RedMountains);
		// Blackwater Rush
		BlackwaterRush.AddAdjTerritory(Harrenhal);
		BlackwaterRush.AddAdjTerritory(StoneySept);
		BlackwaterRush.AddAdjTerritory(Silverhill);
		BlackwaterRush.AddAdjTerritory(KingsLanding);
		BlackwaterRush.AddAdjTerritory(Kingswood);
		BlackwaterRush.AddAdjTerritory(TheMander);
		BlackwaterRush.AddAdjTerritory(SearoadMarshes);
		BlackwaterRush.AddAdjTerritory(Highgarden);
		// The Mander
		TheMander.AddAdjTerritory(Kingswood);
		TheMander.AddAdjTerritory(StormsEnd);
		TheMander.AddAdjTerritory(DornishMarshes);
		TheMander.AddAdjTerritory(BlackwaterRush);
		TheMander.AddAdjTerritory(Highgarden);
		// Searoad Marshes
		SearoadMarshes.AddAdjTerritory(Silverhill);
		SearoadMarshes.AddAdjTerritory(Crakehall);
		SearoadMarshes.AddAdjTerritory(BlackwaterRush);
		SearoadMarshes.AddAdjTerritory(Highgarden);
		// Highgarden
		Highgarden.AddAdjTerritory(DornishMarshes);
		Highgarden.AddAdjTerritory(BlackwaterRush);
		Highgarden.AddAdjTerritory(TheMander);
		Highgarden.AddAdjTerritory(SearoadMarshes);
		Highgarden.AddAdjTerritory(Oldtown);
		Highgarden.AddAdjTerritory(ThreeTowers);
		Highgarden.AddAdjTerritory(RedMountains);
		// Oldtown
		Oldtown.AddAdjTerritory(Wolfswood);
		Oldtown.AddAdjTerritory(CapeKraken);
		Oldtown.AddAdjTerritory(TheTrident);
		Oldtown.AddAdjTerritory(Harlaw);
		Oldtown.AddAdjTerritory(Pyke);
		Oldtown.AddAdjTerritory(CasterlyRock);
		Oldtown.AddAdjTerritory(Highgarden);
		Oldtown.AddAdjTerritory(ThreeTowers);
		Oldtown.AddAdjTerritory(TheArbor);
		// Three Towers
		ThreeTowers.AddAdjTerritory(Highgarden);
		ThreeTowers.AddAdjTerritory(Oldtown);
		ThreeTowers.AddAdjTerritory(TheArbor);
		ThreeTowers.AddAdjTerritory(RedMountains);
		// The Arbor
		TheArbor.AddAdjTerritory(Oldtown);
		TheArbor.AddAdjTerritory(ThreeTowers);
		// Red Mountains
		RedMountains.AddAdjTerritory(DornishMarshes);
		RedMountains.AddAdjTerritory(Highgarden);
		RedMountains.AddAdjTerritory(ThreeTowers);
		RedMountains.AddAdjTerritory(Sandstone);
		RedMountains.AddAdjTerritory(Greenblood);
		// Sandstone
		Sandstone.AddAdjTerritory(RedMountains);
		Sandstone.AddAdjTerritory(Greenblood);
		// Greenblood
		Greenblood.AddAdjTerritory(RedMountains);
		Greenblood.AddAdjTerritory(Sandstone);
		Greenblood.AddAdjTerritory(Sunspear);
		// Sunspear
		Sunspear.AddAdjTerritory(WidowsWatch);
		Sunspear.AddAdjTerritory(WhiteHarbor);
		Sunspear.AddAdjTerritory(Gulltown);
		Sunspear.AddAdjTerritory(Dragonstone);
		Sunspear.AddAdjTerritory(KingsLanding);
		Sunspear.AddAdjTerritory(StormsEnd);
		Sunspear.AddAdjTerritory(Greenblood);
	}

	private void InitializePlayerData() {
		playerColors = new Color[5];
		playerColors[0] = new Color(0.11f, 0.36f, 0.93f); // equivalent to (28, 91, 236)
		playerColors[1] = new Color(1.00f, 0.32f, 0.30f); // equivalent to (255, 82, 76)
		playerColors[2] = new Color(0.08f, 0.84f, 0.24f); // equivalent to (21, 214, 60)
		playerColors[3] = new Color(0.95f, 0.98f, 0.25f); // equivalent to (242, 250, 65)
		playerColors[4] = new Color(0.95f, 0.54f, 0.92f); // equivalent to (241, 137, 234)
		int i;
		for (i = 0; i < 5; i++) {
			playerIDArray[i].GetComponent<PlayerManager>().SetColor(playerColors[i]);
		}
		int playerIDIndex = 2; // 0, 1, 2, 3, 4
		for (i = 0; i < 48; i++) {
			playerIDArray[playerIDIndex].GetComponent<PlayerManager>().AddTerritoryToListInitial(territoryArrayAll[i]);
			playerIDIndex = (playerIDIndex + 1) % 5;
		}

	}

	public void ShuffleTerritoryArray(TerritoryObject[] array) {
		TerritoryObject temp;
		int random;
		for (int i = 0; i < array.Length; i++) {
			random = Random.Range(0, array.Length);
			if (random == i) {
				continue;
			}
			temp = array[i];
			array[i] = array[random];
			array[random] = temp;
		}
	}

	public void ScoreInfo() {
		for (int i = 0; i < info.Length; i++) {
			info[i].text = "P" + (i + 1) + " Territories: " + playerIDArray[i].GetComponent<PlayerManager>().GetNumberOfTerritories();
		}
	}

}
