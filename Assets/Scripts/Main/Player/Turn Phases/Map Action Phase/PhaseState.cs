using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace MapActionPhaseStates
{
    public enum StateType
    {
        Ability,
        Action,
        End,
        Info,
        Neutral,
        None
    }

    public abstract class PhaseState
    {
        public abstract StateType stateType { get; }

        protected PlayerItemManager itemManager;

        public virtual void StartState(StateStartInfo startInfo)
        {
            ClearState();
        }
        public virtual void EndState(StateStartInfo startInfo)
        {
            ClearState();
            eventManager.OnEndPhase(startInfo);
        }
        public virtual void ClearState() { }

        // Clicks
        public virtual void LeftClick(Hex clickHex) { }
        public virtual void RightClick(Hex clickHex) { }
        public virtual void Hover(Hex hoverHex) { }

        // Edge lines
        public int numEdgeLines => GetNumEdgeLines();
        public Dictionary<HexListType, List<LineRenderer>> edgeLines { get; protected set; } = new Dictionary<HexListType, List<LineRenderer>>();

        // Event manager
        public virtual StateEventManager eventManager { get; protected set; } = new StateEventManager();


        // Create edge lines
        protected void CreateHexListEdgeLines(HexListType hexListType, List<Hex> hexList)
        {
            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(hexList);
            List<LineRenderer> lines;
            if (!edgeLines.TryGetValue(hexListType, out lines))
                edgeLines[hexListType] = new List<LineRenderer>();
            lines = edgeLines[hexListType];

            // Create needed lines
            int numNeededLines = edgePoints.Count - lines.Count;
            for (int i = 0; i < numNeededLines; i++)
            {
                LineRenderer line = PathEdge.CreateEdgeLine(hexListType);
                lines.Add(line);
            }

            // Set line points
            for (int i = 0; i < lines.Count; i++)
            {
                if (i < edgePoints.Count)
                {
                    List<Vector3> linePoints = edgePoints[i];
                    PathEdge.DisplayEdgeLine(lines[i], linePoints);
                }
                else
                {
                    lines[i].gameObject.SetActive(false);
                }

            }
        }


        // Hide game object for each edge line
        protected void ClearHexListEdgeLines()
        {
            foreach (List<LineRenderer> lineList in edgeLines.Values)
            {
                foreach (LineRenderer line in lineList)
                {
                    line.gameObject.SetActive(false);
                }
            }
        }


        // Get number of edge lines
        private int GetNumEdgeLines()
        {
            int numEdgeLines = 0;
            foreach (List<LineRenderer> lines in edgeLines.Values)
            {
                numEdgeLines += lines.Count;
            }
            return numEdgeLines;
        }


        // Clear playable hexes for selected card
        protected void ClearPlayableHexes()
        {
            ClearHexListEdgeLines();
        }
    }
}