using FluentArch.Arch.Layer;
using FluentArch.DTO;
using FluentArch.DTO.Rules;
using FluentArch.Utils;

namespace FluentArch.Rules
{
    public class CreateRules
    {
        public bool Create(string namespacePath, IEnumerable<ClassEntityDto> classes)
        {
            var todasCriacoes = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Criacoes));
            var resultado = todasCriacoes.Any(criacao => criacao.Namespace.NamespaceCompare(namespacePath));
            return resultado;
        }

        //TODO: Validar com classes de namespaces diferentes, possivel apos criacao do regex
        public bool Create(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            //TODO: Criar mapper
            var todasEntityDto = layer._classes.Select(x => new EntityDto { Nome = x.Nome, Namespace = x.Namespace, Local = x.Local });
            var todasCriacoes = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Criacoes));
            var resultado = todasCriacoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto.ToList()));
            return resultado;
        }
        public bool OnlyCreate(string namespacePath, IEnumerable<ClassEntityDto> classes)
        {
            var todasCriacoes = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Criacoes));
            var resultado = todasCriacoes.All(criacao => criacao.Namespace.NamespaceCompare(namespacePath));
            return resultado;
        }
        public bool OnlyCreate(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            //TODO: Criar mapper
            var todasEntityDto = layer._classes.Select(x => new EntityDto { Nome = x.Nome, Namespace = x.Namespace, Local = x.Local });
            var todasCriacoes = classes.SelectMany(classe => classe.Funcoes.SelectMany(funcao => funcao.Criacoes));
            var resultado = todasCriacoes.All(criacao => criacao.CompareClassAndNamespace(todasEntityDto.ToList()));
            return resultado;
        }

        //TODO: Validar 
        public bool MustCreate(string namespacePath, IEnumerable<ClassEntityDto> classes)
        {
            var resultado = classes.All(classe => classe.Funcoes.SelectMany(funcao => funcao.Criacoes).Any(criacao => criacao.Namespace.NamespaceCompare(namespacePath)));
            return resultado;
        }
        //TODO: Validar
        public bool MustCreate(Layer layer, IEnumerable<ClassEntityDto> classes)
        {
            var criacoesPorClasse = classes.Select(classe => new CriacoesPorClasse
            {
                Nome = classe.Nome,
                Criacoes = classe.Funcoes.SelectMany(funcao => funcao.Criacoes).ToList(),
            });
            //TODO: Criar mapper
            var todasEntityDto = layer._classes.Select(x => new EntityDto { Nome = x.Nome, Namespace = x.Namespace, Local = x.Local });

            criacoesPorClasse.All(classe => classe.Criacoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto)));
            var resultado = criacoesPorClasse.All(classe => classe.Criacoes.Any(criacao => criacao.CompareClassAndNamespace(todasEntityDto)));
            return resultado;
        }
    }
}
