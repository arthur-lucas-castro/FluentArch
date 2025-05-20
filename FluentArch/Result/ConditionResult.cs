using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Result
{
    public class ConditionResult
    {
        private bool _isSuccessful;
        private IEnumerable<ViolationDto> _violations;

        public bool IsSuccessful => _isSuccessful;
        public IEnumerable<ViolationDto> Violations => _violations;

        public ConditionResult(bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            _violations = new List<ViolationDto>();
        }
        public ConditionResult(bool isSuccessful, ViolationDto violacao)
        {
            _isSuccessful = isSuccessful;
            _violations = new List<ViolationDto> { violacao };
        }
        public ConditionResult(bool isSuccessful, IEnumerable<ViolationDto> violacoes)
        {
            _isSuccessful = isSuccessful;
            _violations = violacoes;
        }
    }
}
