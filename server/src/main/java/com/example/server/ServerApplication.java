package com.example.server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;


@SpringBootApplication
public class ServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(ServerApplication.class, args);
	}


	// @RestController
	// public class rest {
	// 	@GetMapping("/page1")
	// 	public String SayHello(){
	// 		String message = "Người không muốn tới đích, dù khởi đầu tốt thế nào cũng trở nên vô nghĩa";
	// 		System.out.println(message);
	// 		return message;
	// 	}
		
		
	// }

}
 