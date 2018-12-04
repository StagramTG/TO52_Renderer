using System.Collections.Generic;
using Newtonsoft.Json;

public static class Messages
{
    public struct Types
    {
        public const int BEGIN = 0;
        public const int END = 1;
        public const int INIT_DATA = 2;
        public const int SIM_DATA = 3;
    }

    public static string HelloMessage = JsonConvert.SerializeObject(new MessageData<object>(Types.BEGIN, null));
    public static string EndMessage = JsonConvert.SerializeObject(new MessageData<object>(Types.END, null));

    public static string CreateCharacterAgentInitMessage(List<CharacterAgentData> pcharacterAgents)
    {
        return JsonConvert.SerializeObject(new MessageData<List<CharacterAgentData>>(Types.INIT_DATA, pcharacterAgents));
    }
}