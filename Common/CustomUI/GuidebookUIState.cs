using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using BetterThanSlimes.Common.CustomUI;
using System.Collections.Generic;
using System;

public class GuidebookUIState : UIState
{
    public static bool isVisible = false;

    private UIPanel guidebookPanel;
    private UIText pageText;
    private int currentPage = 0;
    private List<string> pages = new List<string>
    {
        "Welcome to the Guidebook! This is page 1.",
        "Page 2: Here you can find useful tips.",
        "Page 3: Explore and conquer the world!",
        "Page 4: Thank you for using this guide!"
    };

    public override void OnInitialize()
    {
        // Main panel
        guidebookPanel = new UIPanel();
        guidebookPanel.SetPadding(10);
        guidebookPanel.Width.Set(400, 0f);
        guidebookPanel.Height.Set(300, 0f);
        guidebookPanel.HAlign = 0.5f;
        guidebookPanel.VAlign = 0.5f;
        Append(guidebookPanel);

        // Page text
        pageText = new UIText(pages[currentPage], 0.8f);
        pageText.Width.Set(0, 1f);
        pageText.Height.Set(0, 1f);
        guidebookPanel.Append(pageText);

        // Previous button
        var prevButton = new UITextPanel<string>("Previous", 0.8f, true);
        prevButton.Width.Set(100, 0f);
        prevButton.Height.Set(30, 0f);
        prevButton.Left.Set(10, 0f);
        prevButton.Top.Set(250, 0f);
        prevButton.OnLeftClick += (evt, element) => ChangePage(-1);
        guidebookPanel.Append(prevButton);

        // Next button
        var nextButton = new UITextPanel<string>("Next", 0.8f, true);
        nextButton.Width.Set(100, 0f);
        nextButton.Height.Set(30, 0f);
        nextButton.Left.Set(290, 0f);
        nextButton.Top.Set(250, 0f);
        nextButton.OnLeftClick += (evt, element) => ChangePage(1);
        guidebookPanel.Append(nextButton);

        // Close button
        var closeButton = new UITextPanel<string>("Close", 0.8f, true);
        closeButton.Width.Set(100, 0f);
        closeButton.Height.Set(30, 0f);
        closeButton.HAlign = 0.5f;
        closeButton.VAlign = 1f;
        closeButton.Top.Set(-30, 0f);
        closeButton.OnLeftClick += (evt, element) =>
        {
            GuidebookUIState.ToggleUI(); // Properly close the UI when the close button is clicked
        };

        guidebookPanel.Append(closeButton);
    }

    public static void ToggleUI()
    {
        var mod = ModContent.GetInstance<MyMod>();
        if (isVisible)
        {
            mod.GuidebookUI.SetState(null); // Close the UI
        }
        else
        {
            mod.GuidebookUI.SetState(new GuidebookUIState()); // Open the UI
        }
        isVisible = !isVisible;
    }


    private void ChangePage(int direction)
    {
        currentPage = Math.Clamp(currentPage + direction, 0, pages.Count - 1);
        pageText.SetText(pages[currentPage]);
    }
}
