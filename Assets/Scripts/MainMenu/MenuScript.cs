using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : PlayerController {

    public Button Play;
    public Button Options;
    public Button Quit;
    public Button Back;

    class newButton
    {
        public Button But;
        public newButton LastBut;
        public newButton NextBut;
    }

    private newButton currentBut;
    private bool canUp, canDown = true;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        IP = GameObject.Find("InputPoller").GetComponent<InputPoller>();

        newButton NPlay = new newButton();
        newButton NOptions = new newButton();
        newButton NQuit = new newButton();

        NPlay.But = Play;
        NPlay.NextBut = NOptions;
        NPlay.LastBut = NQuit;

        NOptions.But = Options;
        NOptions.NextBut = NQuit;
        NOptions.LastBut = NPlay;

        NQuit.But = Quit;
        NQuit.NextBut = NPlay;
        NQuit.LastBut = NOptions;

        currentBut = NPlay;
        currentBut.But.Select();
    }

    public override void Horizontal(float value)
    {

    }

    public override void Vertical(float value)
    {
        if (value == 0)
        {
            canDown = true;
            canUp = true;
        }
        else if(value < 0 && canDown)
        {
            canDown = false;
            currentBut = currentBut.NextBut;
            currentBut.But.Select();
        }
        else if (value > 0 && canUp)
        {
            canUp = false;
            currentBut = currentBut.LastBut;
            currentBut.But.Select();
        }
    }
    public override void Fire1(bool value)
    {
        if(value)currentBut.But.onClick.Invoke();
    }
}
