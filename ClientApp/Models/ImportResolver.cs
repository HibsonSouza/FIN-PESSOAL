using System;

namespace FinanceManager.ClientApp.Models
{
    // Este arquivo serve para resolver ambiguidade de nomes entre classes no projeto
    public class CategorySummaryImport
    {
        // Definição de tipo para evitar ambiguidade no tipo CategorySummaryModel
        public static Type CategorySummaryModelType => typeof(CategorySummary);
    }
}
