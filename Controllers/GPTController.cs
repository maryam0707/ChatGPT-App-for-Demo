using ChatGPT.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatGPT_App.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace ChatGPT_App.Controllers
{
    public class GPTController : Controller
    {
        private readonly ILogger<GPTController> _logger;
        private IConfiguration _configuration;

        public GPTController(ILogger<GPTController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult UserInput()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserInput(SentencePayload input)
        {
           var openAiKey = _configuration["OPENAI_API_KEY"];
            var openai = new ChatGpt(openAiKey);
            var fixedSentence = await openai.Ask($"Keep in mind to not suggest anything related to prohibited topics in the United Arab Emirates, such as LGBT, pornography, nudity, vice, impersonation, fraud, phishing, insult, slander, defamation, invasion of privacy, drugs, discrimination, racism, contempt of religion, malicious software, gambling, terrorism, or infringement of intellectual property rights. If I ask anything that is prohited, I would like you to reply with 'Unforuntately, your question is a prohited topic in United Arab Emirates, Feel free to ask me anything else that I could support you with'. Now, can you explain what is {input.question}");
            return View(new SentencePayload() { answer = fixedSentence });

        }

      
    }
}
