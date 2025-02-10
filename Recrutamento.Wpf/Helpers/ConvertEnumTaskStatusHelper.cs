using Recrutamento.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutamento.Wpf.Helpers
{
    public static class ConvertEnumTaskStatusHelper
    {
        public static string ConvertEnumToString(EnumTaskStatus status)
        {
            switch (status)
            {
                case EnumTaskStatus.Pendente:
                    return "Pendente";
                case EnumTaskStatus.EmProgresso:
                    return "Em Progresso";
                case EnumTaskStatus.Concluida:
                    return "Concluída";
                default:
                    return "Invalida";
            }
        }

        public static EnumTaskStatus ConvertStringToEnum(string status)
        {
            switch (status)
            {
                case "Pendente":
                    return EnumTaskStatus.Pendente;
                case "Em Progresso":
                    return EnumTaskStatus.EmProgresso;
                case "Concluída":
                    return EnumTaskStatus.Concluida;
                default:
                    return EnumTaskStatus.Invalida;
            }
        }
    }
}
