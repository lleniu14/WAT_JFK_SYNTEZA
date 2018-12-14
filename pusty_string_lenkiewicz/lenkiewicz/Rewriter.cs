using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace lenkiewicz {
    class Rewriter : CSharpSyntaxRewriter {
        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node) {

            var firstNode = node.ChildNodes().OfType<VariableDeclarationSyntax>().FirstOrDefault();
            var tmpNode = firstNode.ChildNodes().OfType<PredefinedTypeSyntax>().FirstOrDefault();
            var tmpString = tmpNode.ChildTokens().FirstOrDefault();

            if (tmpString.Kind() == SyntaxKind.StringKeyword) {

                var tmpDeclarator = firstNode.ChildNodes().OfType<VariableDeclaratorSyntax>().FirstOrDefault();

                var tmpId = tmpDeclarator.ChildTokens().FirstOrDefault();

                var ifNull = tmpDeclarator.ChildNodes().OfType<EqualsValueClauseSyntax>().FirstOrDefault();

                if (ifNull == null) {

                    SyntaxNode newNode = SyntaxFactory.VariableDeclaration(SyntaxFactory.PredefinedType(tmpString)).WithVariables(SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(SyntaxFactory.TriviaList(), tmpId.ToString(), SyntaxFactory.TriviaList(SyntaxFactory.Space))).WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Pusty string"))).WithEqualsToken(SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.EqualsToken, SyntaxFactory.TriviaList(SyntaxFactory.Space))))));

                    node = node.ReplaceNode(node, node.ReplaceNode(firstNode, newNode));

                }

            }

            return base.VisitFieldDeclaration(node);
        }

        public override SyntaxNode VisitBlock(BlockSyntax node) {

            IEnumerable<SyntaxNode> fList = new List<SyntaxNode>(node.ChildNodes());

            foreach (SyntaxNode f in fList) {

                var tmpFirst = f.ChildNodes().OfType<VariableDeclarationSyntax>().FirstOrDefault();

                if (tmpFirst != null) {

                    var tmpPredefined = tmpFirst.ChildNodes().OfType<PredefinedTypeSyntax>().FirstOrDefault(); 
                    var tmpString = tmpPredefined.ChildTokens().FirstOrDefault();
                    
                    if (tmpString.Kind() == SyntaxKind.StringKeyword) {

                        var tmpDeclarator = tmpFirst.ChildNodes().OfType<VariableDeclaratorSyntax>().FirstOrDefault();

                        var ifNull = tmpDeclarator.ChildNodes().OfType<EqualsValueClauseSyntax>().FirstOrDefault(); 

                        var tmpId = tmpDeclarator.ChildTokens().FirstOrDefault();

                        if (ifNull == null) {

                            SyntaxNode newNode = SyntaxFactory.VariableDeclaration(SyntaxFactory.PredefinedType(tmpString)).WithVariables(SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(SyntaxFactory.TriviaList(), tmpId.ToString(), SyntaxFactory.TriviaList(SyntaxFactory.Space))).WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Pusty string"))).WithEqualsToken(SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.EqualsToken, SyntaxFactory.TriviaList(SyntaxFactory.Space))))));
                   
                            node = node.ReplaceNode(node, node.ReplaceNode(tmpFirst, newNode));

                            return VisitBlock(node);

                        }

                    }

                }

                }

            return base.VisitBlock(node);

        }


            
        }

    }



           



    


