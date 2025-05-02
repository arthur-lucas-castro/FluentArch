using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Result
{
    public class ConditionResult
    {
        private bool _isSuccessful;
        public bool IsSuccessful { get { return _isSuccessful; } }
        public IEnumerable<ViolationDto> _violacoes;

        public ConditionResult(bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            _violacoes = new List<ViolationDto>();
        }
        public ConditionResult(bool isSuccessful, IEnumerable<ViolationDto> violacoes)
        {
            _isSuccessful = isSuccessful;
            _violacoes = violacoes;
        }
    }
}
