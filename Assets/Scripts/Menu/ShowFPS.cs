using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

public class ShowFPS : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private Dictionary<int, string> CachedNumberStrings = new();

    private int[] frameRateSamples;

    private int cacheNumbersAmount = 300;

    private int averageFromAmount = 30;

    private int averageCounter = 0;

    private int currentAveraged;

    private void Awake()
    {
        // Cache strings and create array
        {
            for (int i = 0; i < cacheNumbersAmount; i++)
            {
                CachedNumberStrings[i] = i.ToString();
            }
            frameRateSamples = new int[averageFromAmount];
        }
    }

    private void Update()
    {
        // Sample
        {
            var currentFrame = (int)Math.Round(1f / Time.smoothDeltaTime); // If your game modifies Time.timeScale, use unscaledDeltaTime and smooth manually (or not).
            frameRateSamples[averageCounter] = currentFrame;
        }

        // Average
        {
            var average = 0f;

            foreach (var frameRate in frameRateSamples)
            {
                average += frameRate;
            }

            currentAveraged = (int)Math.Round(average / averageFromAmount);
            averageCounter = (averageCounter + 1) % averageFromAmount;
        }

        // Assign to UI
        {
            text.text = currentAveraged switch
            {
                var x when x >= 0 && x < cacheNumbersAmount => $"FPS: {CachedNumberStrings[x]}",
                var x when x >= cacheNumbersAmount => $"FPS: > {cacheNumbersAmount}",
                var x when x < 0 => "FPS: < 0",
                _ => "?"
            };
        }
    }
}