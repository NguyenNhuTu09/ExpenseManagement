package com.example.server.rest;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;


@RestController
public class FunRestController {
     @GetMapping("/page01")
     public String sayHello(){
          return "Hello world";
     }
     
}
