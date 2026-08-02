using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNet.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{

    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNamber}")]
    public IActionResult Sum(string firstNumber, string secondNamber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNamber))
        {
            var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNamber);
            return Ok(sum.ToString());

        }
        return BadRequest("Invalid input");
    }

    [HttpGet("subtract/{firstNumber}/{secondNamber}")]
    public IActionResult Subtract(string firstNumber, string secondNamber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNamber))
        {
            var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNamber);
            return Ok(sub.ToString());

        }
        return BadRequest("Invalid input");
    }

    [HttpGet("multiplicator/{firstNumber}/{secondNamber}")]
    public IActionResult Muiltiplicator(string firstNumber, string secondNamber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNamber))
        {
            var multi = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNamber);
            return Ok(multi.ToString());

        }
        return BadRequest("Invalid input");
    }

    [HttpGet("divider/{firstNumber}/{secondNamber}")]
    public IActionResult Divider(string firstNumber, string secondNamber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNamber))
        {
            if(ConvertToDecimal(secondNamber) == 0)
            {
                return BadRequest("Invalid input.");
            }
            var divi = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNamber);
            
            return Ok(divi.ToString());

        }
        return BadRequest("Invalid input");
    }

    [HttpGet("avarage/{firstNumber}/{secondNamber}")]
    public IActionResult Avarage(string firstNumber, string secondNamber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNamber))
        {
            var av = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNamber))/2;
            return Ok(av.ToString());

        }
        return BadRequest("Invalid input");
    
    }


    [HttpGet("sqr/{numb}")]
    public IActionResult Sqr(string numb)
    {
        if (IsNumeric(numb))
        {
            double num;

            double.TryParse(numb, out num);

            var sqr = Math.Sqrt(num);
            return Ok(sqr.ToString());

        }
        return BadRequest("Invalid input");
    }

    private decimal ConvertToDecimal(string strNumber)
    {
        decimal decimalValue;
        if(decimal.TryParse(strNumber, out decimalValue))
        {
            return decimalValue;
        }
        return 0;
    }

    private bool IsNumeric(string strNumber)
    {
        double number;

        bool isNumber = double.TryParse(
            strNumber, 
            System.Globalization.NumberStyles.Any, 
            System.Globalization.NumberFormatInfo.InvariantInfo, 
            out number);

        return isNumber;
    }
}
