using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class CampaignManager
    {
        static public void ContinueCampaign()
        {
            FrameworkCore.level.ClearAll();
            
            CampaignLoad campaignLoad = FrameworkCore.storagemanager.LoadAdventure();
            // restore world map (= FrameworkCore.worldMap)
            WorldMap worldMap = campaignLoad.worldMap;
            // - restore PlayerCommander (= FrameworkCore.player[0])
            FrameworkCore.players[0].inventoryItems = campaignLoad.playerCommander.inventoryItems;
            FrameworkCore.players[0].campaignShips = campaignLoad.playerCommander.campaignShips;
            // - restore events (= FrameworkCore.eventManager)
            LoadEvents(campaignLoad.eventLoad, worldMap.evManager);

            FrameworkCore.worldMap = worldMap;
            FrameworkCore.worldMap.cloudManager = new CloudManager();
            FrameworkCore.gameState = GameState.WorldMap;
            FrameworkCore.worldMap.EnterMap();
        }

        static private void LoadEvents(EventLoad eventLoad, EventManager evManager)
        {
            evManager.kToucansOnboard = eventLoad.kToucansOnboard;
            evManager.kPandaOnboard = eventLoad.kPandaOnboard;
            evManager.kCrisiumOnBoard = eventLoad.kCrisiumOnBoard;
            evManager.kHaveGauntlet = eventLoad.kHaveGauntlet;
            evManager.tradeItems = eventLoad.tradeItems;
            evManager.Logs = eventLoad.Logs;
            evManager.inventoryPool = eventLoad.inventoryPool;

            evManager.eventPool = eventLoad.eventPool;
            evManager.dangerPool = eventLoad.dangerPool;
            evManager.wormPool = eventLoad.wormPool;
            evManager.unlockableEventPool = eventLoad.unlockableEventPool;

            InitializeEvents(new List<List<Event>>
            {
                evManager.eventPool,
                evManager.dangerPool,
                evManager.wormPool,
                evManager.unlockableEventPool
            },
            evManager);
        }

        private static void InitializeEvents(List<List<Event>> eventList, EventManager evManager)
        {
            foreach (List<Event> events in eventList)
            {
                foreach (Event ev in events)
                {
                    ev.manager = evManager.menuManager;
                    ev.eventManager = evManager;
                }
            }
        }
    }
}
