using System;
using System.Collections.Generic;
using System.Text;

namespace TestLib
{
    public class SqlGenerator
    {
        private List<Clause> clauses;
        private StringBuilder stringBuilder;

        public SqlGenerator(List<Clause> clauses)
        {
            //must first step is where
            clauses.Reverse();

            stringBuilder = new StringBuilder();
            this.clauses = clauses;
        }

        public Type NewType { get; set; }
        public Type DefaultType { get; set; }
        public Type JoinType { get; set; }

        public string Generate()
        {
            foreach (var clause in clauses)
            {
                if (clause is WhereClause whereClause)
                {
                    DefaultType = ExpressionTranslate.ParseWhereType(whereClause);
                }
                else if (clause is JoinClause joinClause)
                {
                    JoinType = ExpressionTranslate.ParseJoinType(joinClause);
                }
                else if (clause is SelectClause selectClause)
                {
                    NewType = ExpressionTranslate.ParseNewType(selectClause);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
