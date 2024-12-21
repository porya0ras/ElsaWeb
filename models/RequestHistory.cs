﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workflowTest.models;

public class RequestHistory
{
    [Key]
    public Guid Id{ get; set; }
    public Guid RequestId{ get; set; }
    public bool Action { get; set; }
    public string Comment {  get; set; }
}
