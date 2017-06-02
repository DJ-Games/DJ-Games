using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MiniRogue
{ 
    enum EventCardTurnState
    {
        INITIAL_ROLL,
        ROLLANIMATION,
        SKILL_CHECK,
        ADJUSTOPTION,
        HANDLE_EVENT,
        FIGHT_MONSTER,
        COMBAT,
        COMPLETE,
    }

    class EventCard : Card
    {

        

        public int Option { get; set; }

        public Combat CurrentCombat { get; set; }

        public Dictionary<string, Die> CombatDice { get; set; }

        public Dictionary<string, CheckBox> CheckBoxes { get; set; }

        public int AnimationCounter { get; set; }

        public int SkillCheckResult { get; set; }

        public Die EventDie { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public bool SkillCheck { get; set; }

        EventCardTurnState eventCardTurnState;

        // Constructor


        public EventCard(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons) : base(name, cardTexture, cardBack, buttons)
        {
            eventCardTurnState = new EventCardTurnState();
            CurrentButtons = new List<Button>();
        }

        public EventCard(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons, Dictionary<string, Die> combatDice, Dictionary<string, CheckBox> checkBoxes, Dictionary<string, Texture2D> dieTextures) : base(name, cardTexture, cardBack, buttons)
        {
            eventCardTurnState = new EventCardTurnState();
            CurrentButtons = new List<Button>();
            Buttons = buttons;
            CombatDice = combatDice;
            CheckBoxes = checkBoxes;
            DieTextures = dieTextures;
            EventDie = new Die(DieTextures, 840, 400);
        }

        //---------------------- METHODS -----------------------------

        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;

            switch (eventCardTurnState)
            {
                case EventCardTurnState.INITIAL_ROLL:
                    SkillCheck = false;
                    HandleButtons(player);

                    return false;

                case EventCardTurnState.ROLLANIMATION:

                    if (AnimationCounter < 60)
                    {
                        RollAnimation();
                    }
                    else
                    {
                        if (SkillCheck)
                        {
                            SkillCheckResult = player.playerDice.RollDice();
                            if (SkillCheckResult <= player.Rank)
                            {
                                CurrentButtons.Clear();
                                LoadSkillCheckButtons();
                                eventCardTurnState = EventCardTurnState.ADJUSTOPTION;
                            }
                            else
                            {
                                eventCardTurnState = EventCardTurnState.HANDLE_EVENT;
                            }
                            AnimationCounter = 0;
                        }
                        else
                        {
                            Option = player.playerDice.RollDice();
                            EventDie.CurrentTexture = EventDie.DieTextures["Roll " + Option];
                            LoadInitialRollButtons(Option);

                            eventCardTurnState = EventCardTurnState.SKILL_CHECK;
                            AnimationCounter = 0;
                        }
                    }



                    return false;
                
                case EventCardTurnState.SKILL_CHECK:

                    HandleButtons(player);

                    return false;

                case EventCardTurnState.ADJUSTOPTION:

                    HandleButtons(player);

                    return false;

                case EventCardTurnState.HANDLE_EVENT:

                    HandleButtons(player);

                    return false;

                case EventCardTurnState.FIGHT_MONSTER:

                    HandleButtons(player);


                    return false;

                case EventCardTurnState.COMBAT:

                    if (CurrentCombat.HandleCombat(player, CurrentMouseState, PreviousMouseState, XPos, yPos, false))
                    {
                        eventCardTurnState = EventCardTurnState.COMPLETE;
                    }

                    return false;



                case EventCardTurnState.COMPLETE:

                    return true;

                default:
                    return false;
            }


        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont dungeonFont)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 130), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            switch (eventCardTurnState)
            {
                case EventCardTurnState.INITIAL_ROLL:
                    sBatch.DrawString(dungeonFont, "Roll for event.", new Vector2(500, 200), Color.White);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 575), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

                    break;

                case EventCardTurnState.ROLLANIMATION:

                    EventDie.DrawCombatDie(sBatch);

                    break;

                case EventCardTurnState.SKILL_CHECK:

                    EventDie.DrawCombatDie(sBatch);
                    sBatch.DrawString(dungeonFont, "Roll for skill check.", new Vector2(500, 200), Color.White);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 575), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

                    int counter = 550;
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(counter, 475), new Rectangle?(), Color.White, 0f, new Vector2(), .50f, SpriteEffects.None, 1);
                        counter += 80;
                    }

                    break;

                case EventCardTurnState.ADJUSTOPTION:

                    EventDie.DrawCombatDie(sBatch);
                    int counter2 = 550;
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(counter2, 475), new Rectangle?(), Color.White, 0f, new Vector2(), .50f, SpriteEffects.None, 1);
                        counter2 += 80;
                    }
                    sBatch.DrawString(dungeonFont, "Adjustment", new Vector2(500, 200), Color.White);


                    break;

                case EventCardTurnState.HANDLE_EVENT:

                    EventDie.DrawCombatDie(sBatch);
                    int counter3 = 550;
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(counter3, 475), new Rectangle?(), Color.White, 0f, new Vector2(), .50f, SpriteEffects.None, 1);
                        counter3 += 80;
                    }
                    sBatch.DrawString(dungeonFont, "Failed skill check", new Vector2(500, 200), Color.White);


                    break;

                case EventCardTurnState.FIGHT_MONSTER:


                    sBatch.Draw(Buttons["Combat Button"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case EventCardTurnState.COMBAT:

                    CurrentCombat.DrawCombat(sBatch, dungeonFont);

                    break;

                case EventCardTurnState.COMPLETE:
                    break;
                default:
                    break;
            }

        }

        public void LoadInitialRollButtons(int roll)
        {
            switch (roll)
            {

                case 1:
                    CurrentButtons.Add(Buttons["Found Ration Highlight"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 1;
                    break;

                case 2:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion Highlight"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 2;
                    break;

                case 3:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot Highlight"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 3;
                    break;

                case 4:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone Highlight"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 4;
                    break;

                case 5:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield Highlight"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 5;
                    break;

                case 6:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster Highlight"]);

                    Option = 6;
                    break;

                default:
                    break;
            }
        }

        public void LoadSkillCheckButtons()
        {
            switch (Option)
            {

                case 1:
                    CurrentButtons.Add(Buttons["Found Ration Highlight"]);
                    CurrentButtons.Add(Buttons["Health Potion Highlight"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster Highlight"]);

                    break;

                case 2:

                    CurrentButtons.Add(Buttons["Found Ration Highlight"]);
                    CurrentButtons.Add(Buttons["Health Potion Highlight"]);
                    CurrentButtons.Add(Buttons["Found Loot Highlight"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    Option = 2;
                    break;

                case 3:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion Highlight"]);
                    CurrentButtons.Add(Buttons["Found Loot Highlight"]);
                    CurrentButtons.Add(Buttons["Whetstone Highlight"]);
                    CurrentButtons.Add(Buttons["Found Shield"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    break;

                case 4:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot Highlight"]);
                    CurrentButtons.Add(Buttons["Whetstone Highlight"]);
                    CurrentButtons.Add(Buttons["Found Shield Highlight"]);
                    CurrentButtons.Add(Buttons["Monster"]);

                    break;

                case 5:

                    CurrentButtons.Add(Buttons["Found Ration"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone Highlight"]);
                    CurrentButtons.Add(Buttons["Found Shield Highlight"]);
                    CurrentButtons.Add(Buttons["Monster Highlight"]);

                    break;

                case 6:

                    CurrentButtons.Add(Buttons["Found Ration Highlight"]);
                    CurrentButtons.Add(Buttons["Health Potion"]);
                    CurrentButtons.Add(Buttons["Found Loot"]);
                    CurrentButtons.Add(Buttons["Whetstone"]);
                    CurrentButtons.Add(Buttons["Found Shield Highlight"]);
                    CurrentButtons.Add(Buttons["Monster Highlight"]);

                    break;

                default:
                    break;
            }
        }

        public void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (eventCardTurnState)
                {

                    case EventCardTurnState.INITIAL_ROLL:

                        if (XPos > 700 && XPos < 948 && YPos > 575 && YPos < 648)
                        {

                            eventCardTurnState = EventCardTurnState.ROLLANIMATION;

                        }

                        break;

                    case EventCardTurnState.SKILL_CHECK:

                        if (XPos > 700 && XPos < 948 && YPos > 575 && YPos < 648)
                        {
                            SkillCheck = true;
                            eventCardTurnState = EventCardTurnState.ROLLANIMATION;
                        }

                        break;

                    case EventCardTurnState.ADJUSTOPTION:

                            switch (Option)
                            {
                                case 1:


                                    if (XPos > 550 && XPos < 630 && YPos > 475 && YPos < 555)
                                    {
                                        player.Food++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 630 && XPos < 710 && YPos > 475 && YPos < 555)
                                    {
                                        player.Health += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 950 && XPos < 1030 && YPos > 475 && YPos < 555)
                                    {
                                        eventCardTurnState = EventCardTurnState.FIGHT_MONSTER;
                                    }

                                    break;
                                case 2:


                                    if (XPos > 550 && XPos < 630 && YPos > 475 && YPos < 555)
                                    {
                                        player.Food++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 630 && XPos < 710 && YPos > 475 && YPos < 555)
                                    {
                                        player.Health += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 710 && XPos < 790 && YPos > 475 && YPos < 555)
                                    {
                                        player.Gold++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 3:


                                    if (XPos > 630 && XPos < 710 && YPos > 475 && YPos < 555)
                                    {
                                        player.Health += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 710 && XPos < 790 && YPos > 475 && YPos < 555)
                                    {
                                        player.Gold++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 790 && XPos < 870 && YPos > 475 && YPos < 555)
                                    {
                                        player.Experience += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 4:


                                    if (XPos > 710 && XPos < 790 && YPos > 475 && YPos < 555)
                                    {
                                        player.Gold++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 790 && XPos < 870 && YPos > 475 && YPos < 555)
                                    {
                                        player.Experience += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 870 && XPos < 950 && YPos > 475 && YPos < 555)
                                    {
                                        player.Armor++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 5:


                                    if (XPos > 790 && XPos < 870 && YPos > 475 && YPos < 555)
                                    {
                                        player.Experience += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 870 && XPos < 950 && YPos > 475 && YPos < 555)
                                    {
                                        player.Armor++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 950 && XPos < 1030 && YPos > 475 && YPos < 555)
                                    {
                                        eventCardTurnState = EventCardTurnState.FIGHT_MONSTER;
                                    }

                                    break;
                                case 6:


                                    if (XPos > 870 && XPos < 950 && YPos > 475 && YPos < 555)
                                    {
                                        player.Armor++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    if (XPos > 950 && XPos < 1030 && YPos > 475 && YPos < 555)
                                    {
                                        eventCardTurnState = EventCardTurnState.FIGHT_MONSTER;
                                    }

                                    if (XPos > 550 && XPos < 630 && YPos > 475 && YPos < 555)
                                    {
                                        player.Food++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;

                                default:
                                    break;
                            }
                        


                        break;

                    case EventCardTurnState.HANDLE_EVENT:

                            switch (Option)
                            {
                                case 1:

                                    if (XPos > 550 && XPos < 630 && YPos > 475 && YPos < 555)
                                    {
                                        player.Food++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 2:

                                    if (XPos > 630 && XPos < 710 && YPos > 475 && YPos < 555)
                                    {
                                        player.Health += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 3:

                                    if (XPos > 710 && XPos < 790 && YPos > 475 && YPos < 555)
                                    {
                                        player.Gold++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 4:

                                    if (XPos > 790 && XPos < 870 && YPos > 475 && YPos < 555)
                                    {
                                        player.Experience += 2;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 5:

                                    if (XPos > 870 && XPos < 950 && YPos > 475 && YPos < 555)
                                    {
                                        player.Armor++;
                                        eventCardTurnState = EventCardTurnState.COMPLETE;
                                    }

                                    break;
                                case 6:

                                    if (XPos > 950 && XPos < 1030 && YPos > 475 && YPos < 555)
                                    {
                                        eventCardTurnState = EventCardTurnState.FIGHT_MONSTER;
                                    }

                                    break;

                                default:
                                    break;

                            }

                        break;

                    case EventCardTurnState.FIGHT_MONSTER:

                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {

                            CurrentCombat = new Combat(Buttons, CombatDice, CheckBoxes);
                            eventCardTurnState = EventCardTurnState.COMBAT;

                        }

                        break;

                }
            }

            

        }


        public void RollAnimation()
        {
            EventDie.CurrentTexture = EventDie.DieTextureList[Rng.Next(EventDie.DieTextureList.Count - 1)];
            AnimationCounter += 5;
        }

    }
}
