﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using MessagesManager.Models; 

namespace MessagesManager.Utils
{   
    public class QueueMessage
    {
        string QueueName = Configuration.QuequeMensajes;

        public void SendMessage(int entidadId, int tipoNotificacion, int emisorId, int receptorId, string mensaje)
        {
            if (!MessageQueue.Exists(QueueName))
            {
                MessageQueue.Create(QueueName);
            }

            MessageQueue msg = new MessageQueue(QueueName);            

            Notificacion notificacion = new Notificacion() { 
                tipoNotificacionId = tipoNotificacion,
                entidadId = entidadId,
                emisorId = entidadId,
                receptorId = receptorId,
                mensaje = mensaje,
                fechaEnvio = DateTime.Now,
                leido = false               
            };

            msg.Send(notificacion);
        }

        public string GetMessage()
        {
            string mensaje = string.Empty;
            try
            {
                if (MessageQueue.Exists(QueueName))
                {
                    MessageQueue mq = new MessageQueue(QueueName);

                    mq.MessageReadPropertyFilter.CorrelationId = true;
                    //.Receive(new TimeSpan(0, 0, 3));
                    mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

                    System.Messaging.Message msg = mq.Receive();

                    Notificacion notificacion = new Notificacion();


                    mensaje = msg.Body.ToString();
                }
            }
            catch (MessageQueueException mqe)
            {
                mensaje = "No Message";
                Console.WriteLine(mqe.Message);
            }            
            catch (InvalidOperationException ioe)
            { 
                mensaje = "No Message";
                Console.WriteLine(ioe.Message);               
            }			
            catch (Exception ex)
            {
                mensaje = "No Message";
                Console.WriteLine(ex.Message);    
            }
            return mensaje;
        }

    }
}
