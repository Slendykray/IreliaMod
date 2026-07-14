using IreliaMod.Survivors.Irelia.SkillStates;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(SlashCombo));

            Modules.Content.AddEntityState(typeof(SkillStates.Dash));
            Modules.Content.AddEntityState(typeof(SkillStates.BeginVanguardsEdge));
            Modules.Content.AddEntityState(typeof(SkillStates.VanguardsEdge));
            Modules.Content.AddEntityState(typeof(SkillStates.Shield));
            Modules.Content.AddEntityState(typeof(SkillStates.SlashPassive));
            Modules.Content.AddEntityState(typeof(SkillStates.ThrowBomb));
        }
    }
}
