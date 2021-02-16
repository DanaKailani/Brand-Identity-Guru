

(deftemplate UI-state
   (slot id (default-dynamic (gensym*)))
   (slot display)
   (slot relation-asserted (default none))
   (slot response (default none))
   (multislot valid-answers)
   (slot state (default middle)))
   
(deftemplate state-list
   (slot current)
   (multislot sequence))
  
(deffacts startup
   (state-list))



;;;*************************
;;;* GUI INTERACTION RULES *
;;;*************************

(defrule ask-question

   (declare (salience 5))
   
   (UI-state (id ?id))
   
   ?f <- (state-list (sequence $?s&:(not (member$ ?id ?s))))
             
   =>
   
   (modify ?f (current ?id)
              (sequence ?id ?s))
   
   (halt))

(defrule handle-next-no-change-none-middle-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id)

   ?f2 <- (state-list (current ?id) (sequence $? ?nid ?id $?))
                      
   =>
      
   (retract ?f1)
   
   (modify ?f2 (current ?nid))
   
   (halt))

(defrule handle-next-response-none-end-of-chain

   (declare (salience 10))
   
   ?f <- (next ?id)

   (state-list (sequence ?id $?))
   
   (UI-state (id ?id)
             (relation-asserted ?relation))
                   
   =>
      
   (retract ?f)

   (assert (add-response ?id)))   

(defrule handle-next-no-change-middle-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id ?response)

   ?f2 <- (state-list (current ?id) (sequence $? ?nid ?id $?))
     
   (UI-state (id ?id) (response ?response))
   
   =>
      
   (retract ?f1)
   
   (modify ?f2 (current ?nid))
   
   (halt))

(defrule handle-next-change-middle-of-chain

   (declare (salience 10))
   
   (next ?id ?response)

   ?f1 <- (state-list (current ?id) (sequence ?nid $?b ?id $?e))
     
   (UI-state (id ?id) (response ~?response))
   
   ?f2 <- (UI-state (id ?nid))
   
   =>
         
   (modify ?f1 (sequence ?b ?id ?e))
   
   (retract ?f2))
   
(defrule handle-next-response-end-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id ?response)
   
   (state-list (sequence ?id $?))
   
   ?f2 <- (UI-state (id ?id)
                    (response ?expected)
                    (relation-asserted ?relation))
                
   =>
      
   (retract ?f1)

   (if (neq ?response ?expected)
      then
      (modify ?f2 (response ?response)))
      
   (assert (add-response ?id ?response)))   

(defrule handle-add-response

   (declare (salience 10))
   
   (logical (UI-state (id ?id)
                      (relation-asserted ?relation)))
   
   ?f1 <- (add-response ?id ?response)
                
   =>
      
   (str-assert (str-cat "(" ?relation " " ?response ")"))
   
   (retract ?f1))   

(defrule handle-add-response-none

   (declare (salience 10))
   
   (logical (UI-state (id ?id)
                      (relation-asserted ?relation)))
   
   ?f1 <- (add-response ?id)
                
   =>
      
   (str-assert (str-cat "(" ?relation ")"))
   
   (retract ?f1))   

(defrule handle-prev

   (declare (salience 10))
      
   ?f1 <- (prev ?id)
   
   ?f2 <- (state-list (sequence $?b ?id ?p $?e))
                
   =>
   
   (retract ?f1)
   
   (modify ?f2 (current ?p))
   
   (halt))
	   


;;;first question	   
	   
(defrule initial-question ""
	(logical (start))
	=>
	(assert (UI-state (display StartQuestion)
					  (relation-asserted initial)
					  (response Normal)
					  (valid-answers "Kids" "Health" "Money" "Food"))))
					
;;;For Kids
(defrule kids ""
   (logical (initial Kids))
   =>
   (assert (UI-state (display Kids)
                    (relation-asserted kids_question)
					(response Normal)
					(valid-answers "Ambition" "Positivity" "Growth" "Intelligence"))))

(defrule ambition 
	(logical (kids_question Ambition))
	=>
	(assert (UI-state (display Ambition)
					(state finalPurple))))

(defrule positivity 
	(logical (kids_question Positivity))
	=>
	(assert (UI-state (display Positivity)
					(state finalYellow))))

(defrule growth 
	(logical (kids_question Growth))
	=>
	(assert (UI-state (display Growth)
					(state finalGreen))))

(defrule intelligence 
	(logical (kids_question Intelligence))
	=>
	(assert (UI-state (display Intelligence)
					(state finalBlue))))

;;;Health
(defrule health ""
   (logical (initial Health))
   =>
      (assert (UI-state (display Health)
                     (relation-asserted hospital)
					(response Normal)
					(valid-answers Yes No))))

(defrule hospital-yes ""
   (logical (hospital Yes))
	=>
	(assert (UI-state (display HospitalYes)
					(state finalRed))))

(defrule hospital-no ""
   (logical (hospital No))
	=>
	(assert (UI-state (display HospitalNo)
					(relation-asserted pharmacyclinic)
					(response Normal)
					(valid-answers "Pharmacy" "Clinic"))))

(defrule pharmacy ""
   (logical (pharmacyclinic Pharmacy))
	=>
	(assert (UI-state (display Pharmacy)
					(relation-asserted pharmacypositivity)
					(response Normal)
					(valid-answers Yes No))))

(defrule positivity-yes ""
   (logical (pharmacypositivity Yes))
	=>
	(assert (UI-state (display PositivityYes)
					(state finalYellow))))

(defrule positivity-no ""
   (logical (pharmacypositivity No))
	=>
	(assert (UI-state (display PositivityNo)
					(state finalGreen))))

(defrule clinic ""
   (logical (pharmacyclinic Clinic))
	=>
	(assert (UI-state (display Clinic)
					(relation-asserted clinicslogan)
					(response Normal)
					(valid-answers "Calmness" "Trust"))))

(defrule calmness ""
   (logical (clinicslogan Calmness))
	=>
	(assert (UI-state (display Calmness)
					(state finalBeige))))

(defrule trust ""
   (logical (clinicslogan Trust))
	=>
	(assert (UI-state (display Trust)
					(state finalNavy))))

(defrule money ""
   (logical (initial Money))
	=>
	(assert (UI-state (display Money)
					(relation-asserted categorymoney)
					(response Normal)
					(valid-answers "General Banking" "Online Banking"))))

(defrule banking ""
   (logical (categorymoney General_Banking))
	=>
	(assert (UI-state (display Banking)
					(state finalBlue))))

(defrule online-banking ""
   (logical (categorymoney Online_Banking))
	=>
	(assert (UI-state (display OnlineBanking)
					(relation-asserted safetysuccess)
					(response Normal)
					(valid-answers "Safety" "Success" "Both"))))

(defrule safety ""
   (logical (safetysuccess Safety))
	=>
	(assert (UI-state (display Safety)
					(state finalOrange))))

(defrule success ""
   (logical (safetysuccess Success))
	=>
	(assert (UI-state (display Success)
					(state finalGreen))))

(defrule both ""
   (logical (safetysuccess Both))
	=>
	(assert (UI-state (display Both)
					(state finalOrangeGreen))))

(defrule food ""
   (logical (initial Food))
	=>
	(assert (UI-state (display Food)
					(relation-asserted categoryfood)
					(response Normal)
					(valid-answers "Fast Food" "Dessert" "Healthy Food"))))

(defrule fast_food ""
   (logical (categoryfood Fast_Food))
	=>
	(assert (UI-state (display FastFood)
					(relation-asserted fastfoodslogan)
					(response Normal)
					(valid-answers "Spicy" "Less Aggressive Heat"))))

(defrule spicy ""
   (logical (fastfoodslogan Spicy))
	=>
	(assert (UI-state (display Spicy)
					(state finalRed))))

(defrule less_heat ""
   (logical (fastfoodslogan Less_Aggressive_Heat))
	=>
	(assert (UI-state (display LessHeat)
					(state finalOrange))))

(defrule dessert ""
   (logical (categoryfood Dessert))
	=>
	(assert (UI-state (display Dessert)
					(relation-asserted dessertslogan)
					(response Normal)
					(valid-answers "Royalty" "Very Sweet" "Energetic Sour"))))

(defrule royalty ""
   (logical (dessertslogan Royalty))
	=>
	(assert (UI-state (display Royalty)
					(state finalPurple))))

(defrule very_sweet ""
   (logical (dessertslogan Very_Sweet))
	=>
	(assert (UI-state (display VerySweet)
					(state finalPink))))

(defrule energetic_sour ""
   (logical (dessertslogan Energetic_Sour))
	=>
	(assert (UI-state (display EnergeticSour)
					(state finalRed))))

(defrule healthy_food ""
   (logical (categoryfood Healthy_Food))
	=>
	(assert (UI-state (display HealthyFood)
					(state finalGreenWhiteBeige))))



(defrule system-banner ""
  =>
  (assert (UI-state (display WelcomeMessage)
                    (relation-asserted start)
                    (state initial)
                    (valid-answers))))


(defrule print-suggestion ""
  (declare (salience 10))
  (language ?item)
  =>
  (printout t crlf crlf)
  (printout t "Suggested Language:")
  (printout t crlf crlf)
  (format t " %s%n%n%n" ?item))

