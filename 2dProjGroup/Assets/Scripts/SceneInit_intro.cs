﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneInit_intro : MonoBehaviour {
	public Material cubeMaterial;
	
	//Prefubs
	public Transform Enemy;
	public Transform powerUp_coin;
	public Transform powerUp_health;
	public Transform powerUp_gravity;
	public Transform exitPortal;
	public Transform text;
	
	private Dimension prevDim; //karata to proigoumeno dimension
	private float prevY=0; //krata to  proigoumeno y
	private float prevX=0;
	private float prevZ=0;
	private int count=-15;	//metra ta cube p mpikan stin idia diastasi
	private int countOfList=0; //counter gia to poses listes exoume
	List<Vector3> listPosEnemy = new List<Vector3>();// listes apo pithana pos enemys
	List<List<Vector3>> listOfList = new List<List<Vector3>>(); //lista p krata tis pio pano listes
	private int numOfEnemy; //arithmos ton exthron
	Renderer renderer;
	private int totalEnemyNumber;
	//
	
	// Use this for initialization
	void Start() {
		totalEnemyNumber = 0;
		
		listOfList.Add(new List<Vector3>());
		
		int x=0, y=0, z=0;
		
		//CREATING THE SCENE
		
		//dimension FRONT
		
		prevDim = Dimension.FRONT;
		
		Vector3 size = new Vector3(1,1,1);
		Vector3 position = new Vector3(0,0,0);

		//proto epipedo
		position = new Vector3 (-5,0,0);
		for (position.x = -12; position.x<=25; position.x++) {
				createStaticCube(position, size, Dimension.FRONT);
		}

		//dimiourgia epipedou meta to keno
		for (position.x = -17, position.y=0; position.x>-30; position.x--) {
			createStaticCube(position, size, Dimension.FRONT);
		}

		//dimiourgise tixo gia to telos tou epipedou
		for (position.x=-30, position.y=0; position.y<6; position.y++) {
			createStaticCube(position, size, Dimension.FRONT);
		}
		for (position.x=-30, position.y=5; position.x<-27; position.x++) {
			createStaticCube(position, size, Dimension.FRONT);
		}

		//movalbe kivos
		createMovableCube (new Vector3(18, 1, 0), size, Dimension.FRONT);

		//dimiourgia epipedou anevasmeno kata 2
		for (position.x=21, position.y=2; position.x<35; position.x++) {
			createStaticCube(position, size, Dimension.FRONT);
		}

		
		//dimiourgia enallaktikou dromou sto -3  kai 3
		for (position.x=24, position.y=2, position.z=-3;position.z<=3;position.z++) {
			if (position.z == 0) continue;
			createStaticCube (position,size, Dimension.RIGHT);
		}
		for (position.x=24, position.y=2; position.x<=33; position.x++) {
			position.z = -3;
			createStaticCube(position, size, Dimension.FRONT);
			position.z = 3;
			createStaticCube(position, size, Dimension.FRONT);
		}
		for (position.x=33, position.y=2, position.z=-2;position.z<=2;position.z++) {
			if (position.z == 0) continue;
			createStaticCube (position,size, Dimension.RIGHT);
		}


		//dimiourgia gematis skalas
		int Xposition = 32;
		for (position.y=3, position.z=0;position.y<=6;position.y++) {
			for (position.x=28;position.x<=Xposition;position.x++) {
				createStaticCube(position, size, Dimension.FRONT);
			}
			Xposition--;
		}

		//dimourgia dapedou meta ti skala
		for (position.x=28, position.y=7, position.z=0; position.x>5; position.x--) {
			for (position.z=-1;position.z<=1;position.z++) {
				createStaticCube(position, size, Dimension.FRONT);
			}
		}

		//dimiourgia tavaniou
		for (position.x=28, position.y=13, position.z=0; position.x>13; position.x--) {
			for (position.z=-1;position.z<=1;position.z++) {
				createStaticCube(position, size, Dimension.FRONT);
			}
		}

		for (position.x = 20, position.y=8,position.z=0; position.y<=10;position.y++) {
			createStaticCube(position, size, Dimension.FRONT);
		}

		//gravity
		createGravityPowerUp (new Vector3 (21,8,0), Dimension.FRONT);
		
		for (position.x=16, position.y=12,position.z=0; position.y>=10;position.y--) {
			createStaticCube(position, size, Dimension.FRONT);
		}

		//gravity
		createGravityPowerUp (new Vector3 (17,12,0), Dimension.FRONT);


		//allagi diastasis: LEFT

		//dimiourgia dapedou
		int temp = 0;
		for (position.x=6, position.y=7, position.z=2; temp<4; position.z++, temp++) {
			for (position.x=6+temp;position.x<=(12-temp); position.x++) {
				createStaticCube(position, size, Dimension.LEFT);
			}
		}

		for (position.x=9, position.y=7, position.z=2; position.z<25; position.z++) {
			createStaticCube(position, size, Dimension.LEFT);
		}

		//text messages
		createTextMessage (new Vector3(-29, 4, 0), Dimension.FRONT, "Dead-end. Go back!",20f);
		createTextMessage (new Vector3(-13, 5, 0), Dimension.FRONT, "Press Up Arrow\nto jump!",20f);

		createTextMessage (new Vector3(10, 4, 0), Dimension.FRONT, "This cube is movable",20f);
		createTextMessage (new Vector3(18.5f, 5, 0), Dimension.FRONT, "Press C,Z for dimensional rotation", 6f);
		createTextMessage (new Vector3(18.5f, 6, 0), Dimension.FRONT, "Dead-end again. Try pressing X!", 6f);

		createTextMessage (new Vector3(22, 11, 0), Dimension.FRONT, "The red power up reverses gravity rules!", 20f);

		createTextMessage (new Vector3(3, 11, 0), Dimension.FRONT, "Well, use what you have learned now :)", 20f);


		createTextMessage (new Vector3(9, 11, 5), Dimension.LEFT, "You can fire by pressing space!", 20f);



		//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
		if (/*(listOfList[0][0].x==0.0f)&&*/(listOfList[0][0].z==0.0f)&&(listOfList[0].Count>10)){//elexos an ime ontos sto proto diadromo ke an exi toulaxiston 7 cubes
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
			listOfList[0].RemoveRange(0,10); // stin arxi (tou stadiou) afinoume kapies thesis adies 
			//Debug.Log(listOfList[0][0].x + "," + listOfList[0][0].z + "," + listOfList[0].Count);
		}

		//Debug.Log("-----" + listOfList.Count);
		List<int> zeroOrOne = new List<int>();// pithanotita 5/2 gia ton elaxisto aritmo ton exthron se mia grami 1:0
		zeroOrOne.Add(1);
		zeroOrOne.Add(1);
		zeroOrOne.Add(0);
		zeroOrOne.Add(1);
		zeroOrOne.Add(1);
		zeroOrOne.Add(0);
		zeroOrOne.Add(1);
		
		//random dimiourgia exthron
		for (int c=0 ; c<listOfList.Count ; c++){
			if (listOfList[c].Count<=10)// gia ligotera apo 10 cubes epilego metaksi 0 kai 1
			{
				numOfEnemy = Random.Range (0, 2);// se ligoreto apo 10 cubes miono tin pithanotita na vgi ekthros
			}
			else if (listOfList[c].Count<=30)// gia ligotera apo 30 cubes epilego metaksi 0 i 1 me pithanotita na ine 1 5/7 kai ton arithmo to cubes/40
			{
				numOfEnemy = Random.Range (zeroOrOne[Random.Range(0,zeroOrOne.Count)], (Mathf.CeilToInt(listOfList[c].Count / 40.0f))+1); //tixeos arithmos exthron
			}
			else {// gia perisotera apo 30 cubes epilego metaksi 1 kai ton arithmo to cubes/40
				numOfEnemy = Random.Range (1, (Mathf.CeilToInt(listOfList[c].Count / 40.0f))+1); //tixeos arithmos exthron
			}
			for (int i=0; i<numOfEnemy; i++) { //dimiourgia ekthron
				if (listOfList[c].Count>0){
					int p = Random.Range(0, (listOfList[c].Count));
					Transform  newEnemy = Instantiate(Enemy,listOfList[c][p],Quaternion.identity ) as Transform;
					newEnemy.tag="Enemy";
					if (p+2<=listOfList[c].Count-1){
						listOfList[c].RemoveAt(p+2);
					}
					if (p+1<=listOfList[c].Count-1){
						listOfList[c].RemoveAt(p+1);
					}
					
					listOfList[c].RemoveAt(p);
					
					if (p-1>=0){ // p-1
						listOfList[c].RemoveAt(p-1);
					}
					if (p-2>=0){ //p-2
						listOfList[c].RemoveAt(p-2);
					}
				}
			}
			/*Debug.Log("-----" + numOfEnemy);*/
			totalEnemyNumber+=numOfEnemy;
			Debug.Log("totalEnemyNumber:" + totalEnemyNumber);
		}
		
		
	}
	
	
	/*
	 * Function that creates a static cube in the scene
	 * 
	 * Parameters:
	 * - Position: the position of the cube defeined as a vector
	 * - Size: the size of the cube defined as a vector
	 * - Dimension: the dimension of the cube
	 */
	private void createStaticCube(Vector3 position, Vector3 size, Dimension dimension) {
		//
		if ((prevDim != dimension)&&(count>=0)) { //se kathe alagi tou divension midenizete to count , to (count>=0) xrismopoite gia tin arxi, na min ginonte spawn enemy konta s stin arxi
			
			if (listOfList[countOfList].Count!=0){	//elexos an i proigoumeni lista p dimiourgisa den ine adia , an ine pla midenizo to count kai den dimiourgo kenourgia														
				listOfList.Add(new List<Vector3>());
				countOfList++;
			}
			count=0;
		}
		if (prevY!=position.y){ //to prevY xrisimopoiite gia na elekso an vriskome sto idio y me to proigoumeno kivo p dimiourgisa
			count=0;
		}
		if ((dimension==prevDim)&&((dimension==Dimension.BACK) || (dimension==Dimension.FRONT))&& (prevZ!=position.z)){//an ime sti diastasi front i back kai alazi to z tote midenizete to count gt simeni dimiourgo platforma
			count=0;
		}
		if ((dimension==prevDim)&&((dimension==Dimension.LEFT) || (dimension==Dimension.RIGHT))&& (prevX!=position.x)){
			count=0;
		}
		prevDim = dimension;
		prevY=position.y;
		prevX=position.x;
		prevZ=position.z;
		
		count++;
		if (count >= 5) {
			listOfList[countOfList].Add(new Vector3(position.x,position.y+2,position.z));
		}
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		
		Vector3 scale = transform.localScale;
		scale.x = size.x;
		scale.y = size.y;
		scale.z = size.z;
		
		Rigidbody rigidBody = cube.AddComponent<Rigidbody>();
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;
		
		cube.transform.position = new Vector3(position.x, position.y, position.z);
		cube.transform.localScale = scale;
		cube.AddComponent<Cube>();
		cube.tag = "StaticCube";
		
		//renderer = cube.GetComponent<Renderer> ();
		
		cube.GetComponent<Cube>().setDimension(dimension);
	}
	
	/*
	 * Function that creates a static moveable in the scene
	 * 
	 * Parameters:
	 * - Position: the position of the cube defeined as a vector
	 * - Size: the size of the cube defined as a vector
	 * - Dimension: the dimension of the cube
	 * - Color: the color of the cube
	 */
	private void createMovableCube(Vector3 position, Vector3 size, Dimension dimension) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		
		Vector3 scale = transform.localScale;
		scale.x = size.x;
		scale.y = size.y;
		scale.z = size.z;
		
		Rigidbody rigidBody = cube.AddComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.useGravity = true;
		
		//Renderer renderer = cube.GetComponent<Renderer> ();
		
		if (dimension == Dimension.FRONT || dimension == Dimension.BACK) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		}
		else if (dimension == Dimension.RIGHT || dimension == Dimension.LEFT) {
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
		}
		
		cube.transform.position = new Vector3(position.x, position.y, position.z);
		cube.transform.localScale = scale;
		cube.tag = "MovableCube";
		cube.AddComponent<Cube>();
		
		renderer = cube.GetComponent<Renderer> ();
		
		cube.GetComponent<Cube>().setDimension(dimension);
	}
	
	
	/**
	 * Function that creates a coin in the given position
	 * */
	private void createCoinPowerUp(Vector3 position, Dimension dimension) {
		Transform  newCoin = Instantiate(powerUp_coin, position,Quaternion.identity ) as Transform;
		newCoin.tag = "Coin";
		newCoin.Rotate( dimensionToVector(dimension) );
	}
	
	
	/**
	 * Function that creates a gravity power up in the given position
	 * */
	private void createGravityPowerUp(Vector3 position, Dimension dimension) {
		Transform  newGravity = Instantiate(powerUp_gravity, position,Quaternion.identity ) as Transform;
		newGravity.tag = "Gravity";
		newGravity.Rotate( dimensionToVector(dimension) );
	}
	
	
	/**
	 * Function that creates a health power up in the given position
	 * */
	private void createHealthPowerUp(Vector3 position, Dimension dimension) {
		Transform  newHealth = Instantiate(powerUp_health, position,Quaternion.identity ) as Transform;
		newHealth.tag = "Health";
		newHealth.Rotate( dimensionToVector(dimension) );
	}
	
	
	/**
	 * Function that creates an exit portal in the given position
	 * */
	private void createExitPortal(Vector3 position, Dimension dimension) {
		Transform  newExitPortal = Instantiate(exitPortal, position,Quaternion.identity ) as Transform;
		newExitPortal.tag = "ExitPortal";
		newExitPortal.Rotate( dimensionToVector(dimension) );
	}

	/**
	 * Function that creates an exit portal in the given position
	 * */
	private void createTextMessage(Vector3 position, Dimension dimension, string message, float appearanceDistance) {
		Transform  newTextMessage = Instantiate(text, position,Quaternion.identity ) as Transform;
		newTextMessage.Rotate( dimensionToVector(dimension) );
		
		TextMesh textMesh = newTextMessage.gameObject.GetComponent<TextMesh> ();
		textMesh.text = message;

		newTextMessage.gameObject.GetComponent<TextMessage> ().setDimension (dimension);
		newTextMessage.gameObject.GetComponent<TextMessage> ().setAppearanceDistance (appearanceDistance);
	}
	
	/**
	 * Convert the dimension to a vector that correpsonds to the rotation degrees
	 * */
	private Vector3 dimensionToVector(Dimension dimension) {
		if (dimension == Dimension.FRONT) {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		} else if (dimension == Dimension.BACK) {
			return new Vector3 (0.0f, 180.0f, 0.0f);
		} else if (dimension == Dimension.RIGHT) {
			return new Vector3 (0.0f, 270.0f, 0.0f);
		} else if (dimension == Dimension.LEFT) {
			return new Vector3 (0.0f, 90.0f, 0.0f);
		} else {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}
	
	private void createText() {
		GameObject Text = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}