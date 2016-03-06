using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	public Text text;
	public enum States{cell,mirror,lock_0,sheet_0,cell_mirror,sheet_1,key,lock_1,cell_a,freedom};
	public States myStates;

	GameState cell; 
	GameState mirror;
	GameState lock_0;
	GameState sheet_0;
	GameState cell_mirror;
	GameState sheet_1;
	GameState key;
	GameState lock_1;
	GameState cell_a;
	GameState freedom;

	// Use this for initialization
	void Start () {
		myStates=States.cell;
		stateCreate ();
	}

	void stateCreate (){
		cell= new GameState("You are standing in a prison cell, and you want to escape. There are some dirty sheets on the bed, "+
			  				"a mirror on the wall, and the door is locked from the outside.\n\n"+
							"Press S to check sheets; M to view mirror; L to view the lock.",
							KeyCode.S,States.sheet_0,
							KeyCode.M,States.mirror,
							KeyCode.L,States.lock_0);

		mirror= new GameState( "You are watching your face. A ugly face you think. And you rememver sentence on a sheet. \n\n" +
								"Press R to return. Press T to take off mirror. Press S to sheets.",
								KeyCode.R,States.cell,
								KeyCode.T,States.cell_mirror,
								KeyCode.S,States.sheet_0);

		sheet_0= new GameState( "You cant believe you selpt in these thing for 18 years. You think it is time to change the situation. And one of the sheets wrote 'There some secert behind your face.' \n\n"+
								"Press R to return. Press M to mirror, L to view the lock.",
								KeyCode.R,States.cell,
								KeyCode.M,States.mirror,
								KeyCode.L,States.lock_0);

		lock_0= new GameState("This is one of those button locks. The password is only one digit number. There must be some clues around the cell.\n\n" +
								"Press R to Return, Press M to mirror, Press S to sheets.",
								KeyCode.R,States.cell,
								KeyCode.M,States.mirror,
								KeyCode.S,States.sheet_0);

		//=============================cell_a second stage====================================

		cell_mirror= new GameState("You take off the mirror and something drop on the floor. One sheet and one key. You pick up thhis two.\n\n" +
									"Press R to Return, Press S to sheet, Press K to key",
									KeyCode.R,States.cell_a,
									KeyCode.K,States.key,
									KeyCode.S,States.sheet_1);

		key = new GameState("You pick up the key. It looks like using for some locks.\n\n"+
							"Press R to Return, Press S to sheet, Press L to lock.",
							KeyCode.R,States.cell_a,
							KeyCode.L,States.lock_1,
							KeyCode.S,States.sheet_1);

		cell_a = new GameState("You are thinking about the sheet and the key on your hand. They must be used for some places.\n\n"+
			  					"Press S to check sheets, L to view the lock, Press K to key.",
								KeyCode.K,States.key,
								KeyCode.L,States.lock_1,
								KeyCode.S,States.sheet_1);

		sheet_1 = new GameState("The sheet saying:'The only way to get out is becoming a 0.'\n\n"+
			  					"Press R to return, Press L to lock",
								KeyCode.R,States.cell_a,
								KeyCode.L,States.lock_1,
								KeyCode.S,States.sheet_1);
		lock_1=new GameState("'Come on Avery, you can do this!', you soliloquizing.\n\n"+
			  				"Please enter the password; Press R to return",
							KeyCode.R,States.cell_a,
							KeyCode.Alpha0,States.freedom,
							KeyCode.L,States.lock_1);

		freedom=new GameState("You are free! Steven!'\n\n" +
							"Press P to restart game.",
							KeyCode.P,States.cell,
							KeyCode.Alpha0,States.freedom,
							KeyCode.Alpha0,States.freedom);
	}
	
	// Update is called once per frame
	void Update ()
	{
		print (myStates);
		select();
	}

	void select (){
		if (myStates == States.cell) {
			cell.state_all(this);
		} else if (myStates == States.cell_a) {
			cell_a.state_all(this);
		} else if (myStates == States.sheet_0) {
			sheet_0.state_all(this);
		} else if (myStates == States.mirror) {
			mirror.state_all(this);
		} else if (myStates == States.lock_0) {
			lock_0.state_all(this);
		} else if (myStates == States.lock_1) {
			lock_1.state_all(this);
		} else if (myStates == States.key) {
			key.state_all(this);
		} else if (myStates == States.cell_mirror) {
			cell_mirror.state_all(this);
		} else if (myStates == States.sheet_1) {
			sheet_1.state_all(this);
		}else if (myStates == States.freedom) {
			freedom.state_all(this);
		}
	}
}

public class GameState{
	private string stateText;
	private KeyCode opt0;
	private KeyCode opt1;
	private KeyCode opt2;
	private TextController.States sta0;
	private TextController.States sta1;
	private TextController.States sta2;

	public GameState (string stateText,KeyCode opt0,TextController.States sta0,KeyCode opt1,TextController.States sta1,KeyCode opt2,TextController.States sta2){
	    this.stateText=stateText;
		this.opt0=opt0;
		this.opt1=opt1;
		this.opt2=opt2;
		this.sta0=sta0;
		this.sta1=sta1;
		this.sta2=sta2;
	}

	public void state_all (TextController TextController){
		TextController.text.text =stateText;
		if (Input.GetKeyDown (opt0)) {TextController.myStates=sta0;}
		if (Input.GetKeyDown (opt1)) {TextController.myStates=sta1;}
		if (Input.GetKeyDown (opt2)) {TextController.myStates=sta2;}
	}
}