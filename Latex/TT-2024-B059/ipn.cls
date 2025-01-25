\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{ipn}[2023/09/30 IPN thesis submission class]

\LoadClass[12pt, twoside, openany]{book}
\RequirePackage[utf8]{inputenc}
\RequirePackage[table]{xcolor}
\RequirePackage{geometry}
\RequirePackage[export]{adjustbox}
\RequirePackage{titlesec}
\RequirePackage{tabularx}

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

            \vspace{40pt}
            {\fontsize{20}{40}\selectfont\textbf{\textsc{ESCOM\\}}}
            \vspace{60pt}
            {\fontsize{14}{1}\selectfont\textsc{Trabajo Terminal\\}}
            \vspace{20pt}
            {\fontsize{16}{16}\selectfont\textbf{``\@title''\\}}
            \vspace{20pt}
            {\fontsize{14}{1}\selectfont\textsc{2024-B059\\}}

            \vspace{60pt}
            {\fontsize{14}{14}\selectfont\textsc{Presenta:\\}}
            \vspace{10pt}
            {\fontsize{14}{14}\selectfont\textbf{\@author\\}}

            \vspace{60pt}
            \begin{multicols}{2}[{\fontsize{14}{14}\selectfont\textsc{Directores:\\}}][10pt]
            {\fontsize{14}{14}\selectfont\textbf{\@advisor}\\}
   
            {\fontsize{14}{14}\selectfont\textbf{\@coadvisor}}
            \end{multicols}

            \vfill
            \raggedleft{
                \vspace{60pt}
                {\fontsize{12}{12}\selectfont \@academicyear\\}
              }
            \vspace{40pt}
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

