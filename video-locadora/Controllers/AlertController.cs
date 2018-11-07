using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace video_locadora.Controllers
{
    public class AlertController : _Customizado
    {

        public JsonResult Destroy()
        {
            Alert(null, null, null, null); // Reseta todas as variaves da sessão alerta

            return Json("destroyed");
        }
    }
}