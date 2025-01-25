\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{ipn}[2023/09/30 IPN thesis submission class]

\LoadClass[12pt, twoside, openany]{book}
\RequirePackage[utf8]{inputenc}
\RequirePackage[table]{xcolor}
\RequirePackage{geometry}
\RequirePackage[export]{adjustbox}
\RequirePackage{titlesec}
\RequirePackage{tabularx}

\RequirePackage{fancyhdr} % Paquete para personalizar encabezados y pies de página

% Configuración de los encabezados y pies de página
\fancypagestyle{plain}{
    \fancyhf{} % Limpia encabezados y pies de página
    \renewcommand{\headrulewidth}{0pt} % Elimina la línea del encabezado
    \renewcommand{\footrulewidth}{0pt} % Elimina la línea del pie de página
    \fancyfoot[C]{\thepage} % Número de página centrado en el pie
}




\usepackage[spanish]{babel} % Para configuraciones de idioma y fecha en español
\usepackage{datetime}

\definecolor{blue_escom}{RGB}{21, 105, 189}
\definecolor{column_color}{RGB}{170, 172, 237}
\geometry{
    a4paper,
    left=3cm,
    right=2cm,
    top=2cm,
    bottom=2cm
}

\def\schoolname#1{\def\@schoolname{#1}}
\def\degree#1{\def\@degree{#1}}
\def\advisor#1{\def\@advisor{#1}}
\def\coadvisor#1{\def\@coadvisor{#1}}
\def\academicyear#1{\def\@academicyear{#1}}
\coadvisor{}

\setlength{\arrayrulewidth}{1mm}
\renewcommand{\arraystretch}{2.5}

\newdateformat{mexican}{
    \monthname[\THEMONTH]~de \THEYEAR
}

\newcommand{\version}[1]{
    {\Large\textbf{Versión: #1}\par}
}

\newcommand{\fecha}{
    {\large\textbf{\mexican\today}\par}
}



\renewcommand{\maketitle}{
\begin{titlepage}
    \centering
    \begin{adjustbox}{max width=1.2\linewidth, right}
        \begin{minipage}[c][0.99\textheight][t]{0.13\textwidth}
            \centering
            \includegraphics[width=\linewidth]{img/logo-ipn.png} \\
            \vspace{2mm}
            \hskip3pt
            {\color{blue_escom} \vrule width3.5pt height0.75\textheight} 
            \hskip0.5pt
            {\color{blue_escom} \vrule width7.5pt height0.75\textheight} 
            \hskip0.5pt
            {\color{blue_escom} \vrule width3.5pt height0.75\textheight} \\
            \vspace{2mm}
            \centerline{\includegraphics[width=1.3\linewidth]{img/logo-school.png}}
        \end{minipage}
        \begin{minipage}[c][0.99\textheight][t]{0.95\textwidth}
            \centering
            \vspace{20pt}
            {\fontsize{20}{1}\selectfont\textbf{\textsc{Instituto Politécnico Nacional\\}}}
            
            \hfill

            {\fontsize{16}{15}\selectfont\textbf{\textsc{\@schoolname\\}}}

            \softwarelogo{\includegraphics[width=8cm]{Chemistry_Lab}}\\
            {\fontsize{18}{16}\selectfont\textbf{\textsc{\@title\\}}}
            \vspace{75pt}
            {\fontsize{18}{16}\selectfont\textbf{\textsc{\version{1.0}\\}}}
            \vspace{75pt}
            {\fontsize{13}{11}\selectfont\textbf{\textsc{Elaborado por:\\ \@author\\}}}
            \vspace{15pt}
            {\fontsize{10}{9}\selectfont\textbf{\textsc{\fecha\\}}}
        \end{minipage}
    \end{adjustbox}
\end{titlepage}
}

\titleformat{\chapter}[display]
{\bfseries\Large}
{
    \filright
    \textsc{\textcolor{black}{
        \MakeUppercase{\chaptertitlename}
        \Huge
        \thechapter
    }}
}
{1ex}
{\Huge}
[\vspace{1ex}\titlerule]

